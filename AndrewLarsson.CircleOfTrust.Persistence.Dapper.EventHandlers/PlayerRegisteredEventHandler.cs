using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class PlayerRegisteredEventHandler : IEventHandler<PlayerRegisteredEvent> {
		private static readonly string InsertPlayerFromPlayerRegisteredEvent = @"INSERT INTO Player ([Id], [Username], [Password]) VALUES (@AggregateRootId, @Username, @Password);";
		private readonly IDbConnection _dbConnection;

		public PlayerRegisteredEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(PlayerRegisteredEvent playerRegisteredEvent) {
			return _dbConnection.ExecuteAsync(InsertPlayerFromPlayerRegisteredEvent, playerRegisteredEvent);
		}
	}
}
