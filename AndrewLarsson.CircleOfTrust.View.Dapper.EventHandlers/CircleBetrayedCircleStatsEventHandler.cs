using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleBetrayedCircleStatsEventHandler : IEventHandler<CircleBetrayedEvent> {
		private static readonly string UpdateCircleStatsFromCircleBetrayedEvent = @"UPDATE CircleStats SET [IsBetrayed] = 1 WHERE [CircleId] = @CircleId;";
		private readonly IDbConnection _dbConnection;

		public CircleBetrayedCircleStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(CircleBetrayedEvent circleBetrayedEvent) {
			return _dbConnection.ExecuteAsync(UpdateCircleStatsFromCircleBetrayedEvent, circleBetrayedEvent);
		}
	}
}
