using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleBetrayedPlayerStatsEventHandler : IEventHandler<CircleBetrayedEvent> {
		private static readonly string UpdatePlayerStatsFromCircleBetrayedEvent = @"UPDATE PlayerStats SET [BetrayedCircles] = [BetrayedCircles] + 1 WHERE [PlayerId] = @PlayerId;";
		private readonly IDbConnection _dbConnection;

		public CircleBetrayedPlayerStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(CircleBetrayedEvent circleBetrayedEvent) {
			return _dbConnection.ExecuteAsync(UpdatePlayerStatsFromCircleBetrayedEvent, circleBetrayedEvent);
		}
	}
}
