using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class Aaa_CircleBetrayedEventHandler : IEventHandler<CircleBetrayedEvent> {
		private static readonly string InsertBetrayedCircleFromCircleBetrayedEvent = @"INSERT INTO BetrayedCircle ([Id], [CircleId], [PlayerId]) VALUES (@AggregateRootId, @CircleId, @PlayerId);";
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public Aaa_CircleBetrayedEventHandler(CircleOfTrustDapperPersistenceContext persistenceContext) {
			_persistenceContext = persistenceContext;
		}

		public Task HandleAsync(CircleBetrayedEvent circleBetrayedEvent) {
			return _persistenceContext.DbConnection.ExecuteAsync(InsertBetrayedCircleFromCircleBetrayedEvent, circleBetrayedEvent);
		}
	}
}
