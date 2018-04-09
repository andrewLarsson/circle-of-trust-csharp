using System;
using System.Data;
using System.Data.SqlClient;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Infrastructure;
using AndrewLarsson.CircleOfTrust.Persistence.Dapper;
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
				.AddCircleOfTrustInfrastructure(configuration)
				.AddAutomaticCircleOfTrustCommandHandlers(configuration)
				.AddAutomaticCircleOfTrustPersistenceEventHandlers(configuration)
				.AddAutomaticCircleOfTrustViewEventHandlers(configuration)
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

		public static IServiceCollection AddCircleOfTrustInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddTransient<IDbConnection>(serviceProvider => new SqlConnection(@"Data Source=LITTLEBROTHER\LOCALHOST;Initial Catalog=CircleOfTrust;Integrated Security=True"))
				.AddSingleton<IAggregateRootStore<Player>, DapperPlayerStore>()
				.AddSingleton<IAggregateRootStore<Circle>, DapperCircleStore>()
				.AddSingleton<IAggregateRootStore<Member>, DapperMemberStore>()
				.AddSingleton<IAggregateRootStore<BetrayedCircle>, DapperBetrayedCircleStore>()
				.AddSingleton<IEventPublisher, InProcessEventPublisher>()
			;
		}

		public static IServiceCollection AddAutomaticCircleOfTrustCommandHandlers(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddGenericTypeDefinition(typeof(ICommandHandler<>), "AndrewLarsson.CircleOfTrust.AppService".ToAssembly())
			;
		}

		public static IServiceCollection AddAutomaticCircleOfTrustPersistenceEventHandlers(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddGenericTypeDefinition(typeof(IEventHandler<>), "AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers".ToAssembly())
			;
		}

		public static IServiceCollection AddAutomaticCircleOfTrustViewEventHandlers(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddGenericTypeDefinition(typeof(IEventHandler<>), "AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers".ToAssembly())
			;
		}
	}
}
