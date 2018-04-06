using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class CircleBetrayedEventHandler : IEventHandler<CircleBetrayedEvent> {
		private static readonly string InsertBetrayedCircleFromCircleBetrayedEvent = @"INSERT INTO BetrayedCircle ([Id], [CircleId], [PlayerId]) VALUES (@AggregateRootId, @CircleId, @PlayerId);";
		private readonly IDbConnection _dbConnection;

		public CircleBetrayedEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(CircleBetrayedEvent circleBetrayedEvent) {
			return _dbConnection.ExecuteAsync(InsertBetrayedCircleFromCircleBetrayedEvent, circleBetrayedEvent);
		}
	}
}
