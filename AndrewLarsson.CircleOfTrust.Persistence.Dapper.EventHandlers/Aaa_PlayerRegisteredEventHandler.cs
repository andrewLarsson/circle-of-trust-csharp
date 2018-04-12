using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class Aaa_PlayerRegisteredEventHandler : IEventHandler<PlayerRegisteredEvent> {
		private static readonly string InsertPlayerFromPlayerRegisteredEvent = @"INSERT INTO Player ([Id], [Username], [Password]) VALUES (@AggregateRootId, @Username, @Password);";
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public Aaa_PlayerRegisteredEventHandler(CircleOfTrustDapperPersistenceContext persistenceContext) {
			_persistenceContext = persistenceContext;
		}

		public Task HandleAsync(PlayerRegisteredEvent playerRegisteredEvent) {
			return _persistenceContext.DbConnection.ExecuteAsync(InsertPlayerFromPlayerRegisteredEvent, playerRegisteredEvent);
		}
	}
}
