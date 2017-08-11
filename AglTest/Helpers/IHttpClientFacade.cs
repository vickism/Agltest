using System.Threading.Tasks;

namespace AglTest.Helpers
{
	public interface IHttpClientFacade
	{
		Task<T> GetHttp<T>();
	}
}