using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.Common.DependencyInjection {
	public static class ConfigurationServiceCollectionExtensions {
		public static IServiceCollection ConfigureWithOptionsObject<TOptions>(this IServiceCollection serviceCollection, IConfiguration configuration) where TOptions : class {
			return serviceCollection
				.AddSingleton(configuration.GetSection("App:" + typeof(TOptions).Name).Get<TOptions>())
			;
		}

		/*using Microsoft.Extensions.Options;
		private static IServiceCollection ConfigureWithNamedSection<TOptions>(this IServiceCollection serviceCollection, IConfiguration configuration) where TOptions : class {
			return serviceCollection
				.Configure<TOptions>(configuration.GetSection("App:" + typeof(TOptions).Name))
			;
		}

		private static IServiceCollection ConfigureWithOptionsObjectOld<TOptions>(this IServiceCollection serviceCollection, IConfiguration configuration) where TOptions : class, new() {
			return serviceCollection
				.ConfigureWithNamedSection<TOptions>(configuration)
				.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<TOptions>>().Value)
			;
		}*/
	}
}
