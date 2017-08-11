using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Helpers;
using AglTest.Models;
using AglTest.Tests.Builders;
using NSubstitute;
using Xunit;

namespace AglTest.Tests
{
	public class EndToEndTests
	{
		IHttpClientFacade _httpClientFacade;
		ITraceWriter _traceWriter;
		private List<string> _namesOutput = new List<string>();
		Application _sut;

		public EndToEndTests()
		{
			_httpClientFacade = Substitute.For<IHttpClientFacade>();
			_traceWriter = Substitute.For<ITraceWriter>();
			_traceWriter.WhenForAnyArgs(x => x.WriteLine(Arg.Any<string>()))
				.Do(x => _namesOutput.Add(x.ArgAt<string>(0)));
			_sut = new Application(_httpClientFacade, _traceWriter);
		}

		[Fact]
		public async Task ApplicationWorksWithNoDataReturnedFromSource()
		{
			await _sut.RunAsync();

			_traceWriter.Received(1).WriteHeading("Male");
			_traceWriter.Received(1).WriteHeading("Female");
			Assert.Empty(_namesOutput);
			_traceWriter.DidNotReceiveWithAnyArgs().WriteLine(Arg.Any<string>());
		}

		[Fact]
		public async Task OneWomanWithManyCatsIsSortedAndPrinted()
		{
			var ownersReturned =
				new List<Owner>{
					GetFemaleOwnerWith3Cats()
				};
			_httpClientFacade.GetHttp<List<Owner>>().Returns(Task.FromResult(ownersReturned));

			await _sut.RunAsync();

			Assert.Equal(3,_namesOutput.Count);
			Assert.Equal("1", _namesOutput.First());
			Assert.Equal("3", _namesOutput.Last());
		}

		[Fact]
		public async Task DogsAreIngored()
		{
			var ownersReturned =
				new List<Owner>{
					GetFemaleOwnerWith3Cats()
				};
			ownersReturned.First().Pets.Add(
				new PetBuilder().AsADog().WithName("4").Build());
			_httpClientFacade.GetHttp<List<Owner>>().Returns(Task.FromResult(ownersReturned));

			await _sut.RunAsync();

			Assert.Equal(3, _namesOutput.Count);
			Assert.Equal("1", _namesOutput.First());
			Assert.Equal("3", _namesOutput.Last());
		}

		private static Owner GetFemaleOwnerWith3Cats()
		{
			return new OwnerBuilder().WithGender(GenderEnum.Female).WithPets(Get3Cats()).Build();
		}

		private static List<Pet> Get3Cats()
		{
			return new List<Pet>()
			{
				new PetBuilder().AsACat().WithName("3").Build(),
				new PetBuilder().AsACat().WithName("2").Build(),
				new PetBuilder().AsACat().WithName("1").Build()
			};
		}
	}
}