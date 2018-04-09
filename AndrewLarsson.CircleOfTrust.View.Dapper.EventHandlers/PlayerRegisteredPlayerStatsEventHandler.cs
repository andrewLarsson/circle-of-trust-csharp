using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class PlayerRegisteredPlayerStatsEventHandler : IEventHandler<PlayerRegisteredEvent> {
		private static readonly string InsertPlayerStatsFromPlayerRegisteredEvent = @"INSERT INTO PlayerStats ([PlayerId], [Username], [HasCircle], [MemberOfCircles], [BetrayedCircles]) VALUES (@AggregateRootId, @Username, 0, 0, 0);";
		private readonly IDbConnection _dbConnection;

		public PlayerRegisteredPlayerStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(PlayerRegisteredEvent playerRegisteredEvent) {
			return _dbConnection.ExecuteAsync(InsertPlayerStatsFromPlayerRegisteredEvent, playerRegisteredEvent);
		}
	}
}
