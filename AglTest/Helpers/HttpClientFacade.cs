using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AglTest.Models;
using Newtonsoft.Json;

namespace AglTest.Helpers
{
    public class HttpClientFacade :IHttpClientFacade
    {
	    private readonly IConfigFacade _configFacade;
	    private readonly HttpClient _httpClient;

	    public HttpClientFacade(IConfigFacade configFacade, HttpClient httpClient)
	    {
		    _configFacade = configFacade;
		    _httpClient = httpClient;
	    }

	    public async Task<T> GetHttp<T>()
	    {
		    
		    var responseMessage = await _httpClient.GetAsync(_configFacade.AglUrl);
		    var contents = await responseMessage.Content.ReadAsStringAsync();
		    return JsonConvert.DeserializeObject<T>(contents, new JsonSerializerSettings(){Converters = new List<JsonConverter>(){new PetJsonConverter()}});
	    }
	}
}
