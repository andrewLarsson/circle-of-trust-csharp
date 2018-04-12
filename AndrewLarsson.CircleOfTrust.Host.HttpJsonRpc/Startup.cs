using AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc.DependencyInjection;
using AndrewLarsson.Common.DependencyInjection;
using AndrewLarsson.Common.Host.HttpJsonRpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc {
	public class Startup {
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services) {
			IConfiguration configuration = ConfigurationService.GetConfigurationJson("appsettings.json");
			services.AddJsonRpcWithWebSocketsSupport(config => {
				config.ShowServerExceptions = true;
				config.JsonSerializerSettings = new JsonSerializerSettings() {
					ContractResolver = new CamelCasePropertyNamesContractResolver(),
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				};
			});
			services.AddCircleOfTrust(configuration);
		}

		public void Configure(IApplicationBuilder application, IHostingEnvironment environment) {
			application.UseJsonRpcWithWebSocketsSupport();
		}
	}
}
