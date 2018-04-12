using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleInitiatedPlayerStatsEventHandler : IEventHandler<CircleInitiatedEvent> {
		private static readonly string UpdatePlayerStatsFromCircleInitiatedEvent = @"UPDATE PlayerStats SET [HasCircle] = 1 WHERE [PlayerId] = @PlayerId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public CircleInitiatedPlayerStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(CircleInitiatedEvent circleInitiatedEvent) {
			return _viewContext.DbConnection.ExecuteAsync(UpdatePlayerStatsFromCircleInitiatedEvent, circleInitiatedEvent);
		}
	}
}
