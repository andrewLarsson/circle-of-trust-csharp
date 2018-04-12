using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class Aaa_CircleInitiatedEventHandler : IEventHandler<CircleInitiatedEvent> {
		private static readonly string InsertCircleFromCircleInitiatedEvent = @"INSERT INTO Circle ([Id], [PlayerId], [Name], [Key]) VALUES (@AggregateRootId, @PlayerId, @Name, @Key);";
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public Aaa_CircleInitiatedEventHandler(CircleOfTrustDapperPersistenceContext persistenceContext) {
			_persistenceContext = persistenceContext;
		}

		public Task HandleAsync(CircleInitiatedEvent circleInitiatedEvent) {
			return _persistenceContext.DbConnection.ExecuteAsync(InsertCircleFromCircleInitiatedEvent, circleInitiatedEvent);
		}
	}
}
