using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleInitiatedCircleStatsEventHandler : IEventHandler<CircleInitiatedEvent> {
		private static readonly string InsertCircleStatsFromCircleInitiatedEvent = @"INSERT INTO CircleStats ([CircleId], [PlayerId], [Name], [IsBetrayed], [Members]) VALUES (@AggregateRootId, @PlayerId, @Name, 0, 0);";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public CircleInitiatedCircleStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(CircleInitiatedEvent circleInitiatedEvent) {
			return _viewContext.DbConnection.ExecuteAsync(InsertCircleStatsFromCircleInitiatedEvent, circleInitiatedEvent);
		}
	}
}
