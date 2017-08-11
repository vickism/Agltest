using System.Net.Http;
using System.Threading.Tasks;
using AglTest.Helpers;
using AglTest.Models;
using AglTest.Tests.Builders;
using NSubstitute;
using Xunit;

namespace AglTest.Tests
{
    public class HttpClientFacadeTests
    {
	    readonly string _testUrl = "http://testurl.com";
		[Fact]
        public async Task OwnerWithoutPetsIsDeserializedCorrectly()
        {
	        var configFacade = Substitute.For<IConfigFacade>();
	        configFacade.AglUrl.Returns(_testUrl);
	        var responseText = new OwnerBuilder().WithName("simon").WithGender(GenderEnum.Male).BuildAsJson();

			var httpClient = SetupHttpClient(responseText);


	        var sut = new HttpClientFacade(configFacade, httpClient);
	        var owner = await sut.GetHttp<Owner>();
			Assert.True(owner.GenderIs(GenderEnum.Male));
			Assert.Equal("simon",owner.Name);
        }

	    private static HttpClient SetupHttpClient(string responseText)
	    {
		    var responseMessage = new HttpResponseMessage();

		    responseMessage.Content = new FakeHttpContent(responseText);

		    var messageHandler = new FakeHttpMessageHandler(responseMessage);

		    var httpClient = new HttpClient(messageHandler);
		    return httpClient;
	    }

	    [Fact]
	    public async Task PetCatsAreCastAsCats()
	    {
			var cat = new PetBuilder().AsACat().WithName("tammy").Build();
		    var responseText = new OwnerBuilder().WithName("simon").WithPet(cat).WithGender(GenderEnum.Male).BuildAsJson();

			var configFacade = Substitute.For<IConfigFacade>();
		    configFacade.AglUrl.Returns(_testUrl);

		    var httpClient = SetupHttpClient(responseText);

			var sut = new HttpClientFacade(configFacade, httpClient);
		    var owner = await sut.GetHttp<Owner>();
		    Assert.True(owner.GenderIs(GenderEnum.Male));
		    Assert.Equal("simon", owner.Name);
	    }
	}
}
