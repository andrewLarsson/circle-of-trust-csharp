using System;
using System.Data.SqlClient;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using AndrewLarsson.CircleOfTrust.Domain.Rules;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.CircleOfTrust.Infrastructure;
using AndrewLarsson.CircleOfTrust.Persistence.Dapper;
using AndrewLarsson.CircleOfTrust.Persistence.Dapper.Repositories;
using AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores;
using AndrewLarsson.CircleOfTrust.View.Dapper;
using AndrewLarsson.Common.AppService;
using AndrewLarsson.Common.DependencyInjection;
using AndrewLarsson.Common.Domain;
using AndrewLarsson.Common.Host.Providers;
using AndrewLarsson.Common.Host.Providers.DependencyInjection;
using AndrewLarsson.Common.View;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc.DependencyInjection {
	public static class CircleOfTrustServiceCollectionExtensions {
		public static IServiceCollection AddCircleOfTrust(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddCircleOfTrustDomainServices(configuration)
				.AddCircleOfTrustDomainRules(configuration)
				.AddCircleOfTrustHost(configuration)
				.AddCircleOfTrustDapperPersistence(configuration)
				.AddCircleOfTrustDapperView(configuration)
				.AddCircleOfTrustInProcessEvents(configuration)
				.AddCircleOfTrustAppServiceCommandHandlers(configuration)
			;
		}

		public static IServiceCollection AddCircleOfTrustDomainServices(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddTransient<RegisterPlayerService>()
				.AddTransient<InitiateCircleService>()
				.AddTransient<JoinCircleService>()
				.AddTransient<BetrayCircleService>()
			;
		}

		public static IServiceCollection AddCircleOfTrustDomainRules(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddTransient<CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule>()
				.AddTransient<CirclesMustHaveAUniqueNameRule>()
				.AddTransient<PlayersMayNotBetrayCircleTheyAreAMemberOfRule>()
				.AddTransient<PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule>()
				.AddTransient<PlayersMayNotJoinOrBetrayTheirOwnCircleRule>()
				.AddTransient<PlayersMayOnlyInitiateOneCircleRule>()
				.AddTransient<PlayersMayOnlyJoinACircleOnceRule>()
				.AddTransient<PlayersMustHaveAUniqueUsernameRule>()
			;
		}

		public static IServiceCollection AddCircleOfTrustHost(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddSingleton<ICommandHandlerProvider, CommandHandlerProvider>()
				.AddSingleton<IQueryHandlerProvider, QueryHandlerProvider>()
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
				.AddTransient(serviceProvider => new CircleOfTrustDapperPersistenceContext(new SqlConnection(configuration["App:CircleOfTrustDatabaseConnectionString"])))
				.AddTransient<IAggregateRootStore<Player>, DapperPlayerStore>()
				.AddTransient<IAggregateRootStore<Circle>, DapperCircleStore>()
				.AddTransient<IAggregateRootStore<Member>, DapperMemberStore>()
				.AddTransient<IAggregateRootStore<BetrayedCircle>, DapperBetrayedCircleStore>()
				.AddTransient<IPlayerRepository, DapperPlayerRepository>()
				.AddTransient<ICircleRepository, DapperCircleRepository>()
				.AddTransient<IMemberRepository, DapperMemberRepository>()
				.AddTransient<IBetrayedCircleRepository, DapperBetrayedCircleRepository>()
				.AddTransientGenericTypeDefinition(typeof(IEventHandler<>), "AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers".ToAssembly())
			;
		}

		public static IServiceCollection AddCircleOfTrustDapperView(this IServiceCollection serviceCollection, IConfiguration configuration) {
			if (serviceCollection == null) {
				throw new ArgumentNullException(nameof(serviceCollection));
			}
			return serviceCollection
				.AddTransient(serviceProvider => new CircleOfTrustDapperViewContext(new SqlConnection(configuration["App:CircleOfTrustViewDatabaseConnectionString"])))
				.AddTransientGenericTypeDefinition(typeof(IEventHandler<>), "AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers".ToAssembly())
				.AddTransientGenericTypeDefinition(typeof(IQueryHandler<>), "AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers".ToAssembly())
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
				.AddTransientGenericTypeDefinition(typeof(ICommandHandler<>), "AndrewLarsson.CircleOfTrust.AppService.CommandHandlers".ToAssembly())
			;
		}
	}
}
