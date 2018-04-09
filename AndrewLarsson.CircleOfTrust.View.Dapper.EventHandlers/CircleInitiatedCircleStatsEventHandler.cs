using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleInitiatedCircleStatsEventHandler : IEventHandler<CircleInitiatedEvent> {
		private static readonly string InsertCircleStatsFromCircleInitiatedEvent = @"INSERT INTO CircleStats ([CircleId], [PlayerId], [Name], [IsBetrayed], [Members]) VALUES (@AggregateRootId, @PlayerId, @Name, 0, 0);";
		private readonly IDbConnection _dbConnection;

		public CircleInitiatedCircleStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(CircleInitiatedEvent circleInitiatedEvent) {
			return _dbConnection.ExecuteAsync(InsertCircleStatsFromCircleInitiatedEvent, circleInitiatedEvent);
		}
	}
}
