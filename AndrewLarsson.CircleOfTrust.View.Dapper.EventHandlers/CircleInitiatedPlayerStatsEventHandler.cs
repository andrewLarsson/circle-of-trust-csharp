using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleInitiatedPlayerStatsEventHandler : IEventHandler<CircleInitiatedEvent> {
		private static readonly string UpdatePlayerStatsFromCircleInitiatedEvent = @"UPDATE PlayerStats SET [HasCircle] = 1 WHERE [PlayerId] = @PlayerId;";
		private readonly IDbConnection _dbConnection;

		public CircleInitiatedPlayerStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(CircleInitiatedEvent circleInitiatedEvent) {
			return _dbConnection.ExecuteAsync(UpdatePlayerStatsFromCircleInitiatedEvent, circleInitiatedEvent);
		}
	}
}
