using System.Net.Http;
using System.Threading.Tasks;
using AglTest.Helpers;
using Autofac;

namespace AglTest
{
    class Program
    {
	    static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

		static async Task MainAsync(string[] args)
        {
	        var container = SetupDependencies();
	        using (var scope = container.BeginLifetimeScope())
	        {
		        var app = scope.Resolve<IApplication>();
				await app.RunAsync();
	        }
        }

	    private static IContainer SetupDependencies()
	    {
		    var builder = new ContainerBuilder();
		    builder.RegisterType<ConfigFacade>().As<IConfigFacade>().SingleInstance();
		    builder.RegisterType<HttpClientFacade>().As<IHttpClientFacade>();
		    builder.RegisterType<Application>().As<IApplication>();
		    builder.RegisterType<HttpClient>();
		    builder.RegisterType<ConsoleWriter>().As<ITraceWriter>().SingleInstance();
		    return builder.Build();
	    }
    }
}