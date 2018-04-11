using System;
using System.Data;
using System.Data.SqlClient;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using AndrewLarsson.CircleOfTrust.Infrastructure;
using AndrewLarsson.CircleOfTrust.Persistence.Dapper.Repositories;
using AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores;
using AndrewLarsson.Common.AppService;
using AndrewLarsson.Common.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc.DependencyInjection {
	public static class CircleOfTrustServiceCollectionExtensions {
		public static IServiceCollection AddCircleOfTrust(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddCircleOfTrustHost(configuration)
				.AddCircleOfTrustDapperPersistence(configuration)
				.AddCircleOfTrustInProcessEvents(configuration)
				.AddCircleOfTrustAppServiceCommandHandlers(configuration)
			;
		}

		public static IServiceCollection AddCircleOfTrustHost(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddSingleton<ICommandHandlerProvider, CommandHandlerProvider>()
				.AddSingleton<IEventHandlerProvider, EventHandlerProvider>()
				.AddSingleton<IPlayerVerificationService, PlayerVerificationService>()
				.AddSingleton<IPlayerAuthenticationService, PlayerAuthenticationService>()
			;
		}

		public static IServiceCollection AddCircleOfTrustDapperPersistence(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddTransient<IDbConnection>(serviceProvider => new SqlConnection(@"Data Source=LITTLEBROTHER\LOCALHOST;Initial Catalog=CircleOfTrust;Integrated Security=True"))
				.AddSingleton<IAggregateRootStore<Player>, DapperPlayerStore>()
				.AddSingleton<IAggregateRootStore<Circle>, DapperCircleStore>()
				.AddSingleton<IAggregateRootStore<Member>, DapperMemberStore>()
				.AddSingleton<IAggregateRootStore<BetrayedCircle>, DapperBetrayedCircleStore>()
				.AddSingleton<IPlayerRepository, DapperPlayerRepository>()
				.AddSingleton<ICircleRepository, DapperCircleRepository>()
				.AddSingleton<IMemberRepository, DapperMemberRepository>()
				.AddSingleton<IBetrayedCircleRepository, DapperBetrayedCircleRepository>()
				.AddGenericTypeDefinition(typeof(IEventHandler<>), "AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers".ToAssembly())
				.AddGenericTypeDefinition(typeof(IEventHandler<>), "AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers".ToAssembly())
			;
		}

		public static IServiceCollection AddCircleOfTrustInProcessEvents(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddSingleton<IEventPublisher, InProcessEventPublisher>()
			;
		}

		public static IServiceCollection AddCircleOfTrustAppServiceCommandHandlers(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddGenericTypeDefinition(typeof(ICommandHandler<>), "AndrewLarsson.CircleOfTrust.AppService".ToAssembly())
			;
		}
	}
}
