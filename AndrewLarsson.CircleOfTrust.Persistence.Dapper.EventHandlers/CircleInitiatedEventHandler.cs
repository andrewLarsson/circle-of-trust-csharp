using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class CircleInitiatedEventHandler : IEventHandler<CircleInitiatedEvent> {
		private static readonly string InsertCircleFromCircleInitiatedEvent = @"INSERT INTO Circle ([Id], [PlayerId], [Name], [Key]) VALUES (@AggregateRootId, @PlayerId, @Name, @Key);";
		private readonly IDbConnection _dbConnection;

		public CircleInitiatedEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(CircleInitiatedEvent circleInitiatedEvent) {
			return _dbConnection.ExecuteAsync(InsertCircleFromCircleInitiatedEvent, circleInitiatedEvent);
		}
	}
}
