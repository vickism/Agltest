using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Helpers;
using AglTest.Models;

namespace AglTest
{
	public class Application : IApplication
	{
		private readonly IHttpClientFacade _httpClientFacade;
		private readonly ITraceWriter _traceWriter;

		public Application(IHttpClientFacade httpClientFacade, ITraceWriter traceWriter)
		{
			_httpClientFacade = httpClientFacade;
			_traceWriter = traceWriter;
		}

		public async Task RunAsync()
		{
			var owners = await _httpClientFacade.GetHttp<List<Owner>>() ?? new List<Owner>();

			var maleOwnedCats = GetCatNames(owners, GenderEnum.Male);
			var femaleOwnedCats = GetCatNames(owners, GenderEnum.Female);

			SortAndPrint("Male", maleOwnedCats);
			SortAndPrint("Female", femaleOwnedCats);

			_traceWriter.ReadKey();
		}

		private List<string> GetCatNames(List<Owner> owners, GenderEnum gender)
		{
			var petNames = new List<string>();
			var petOwners = owners.Where(owner => owner.Cats.Count > 0 && owner.GenderIs(gender));
			petOwners.ForEach(owner => petNames.AddRange(owner.Cats.Select(cat => cat.Name)));
			return petNames;
		}

		private void SortAndPrint(string heading, List<string> listToPrint)
		{
			_traceWriter.WriteHeading(heading);
			listToPrint.Sort();
			listToPrint.ForEach(_traceWriter.WriteLine);
			
		}
	}
}