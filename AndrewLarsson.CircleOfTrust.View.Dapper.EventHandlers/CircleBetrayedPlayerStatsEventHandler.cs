using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleBetrayedPlayerStatsEventHandler : IEventHandler<CircleBetrayedEvent> {
		private static readonly string UpdatePlayerStatsFromCircleBetrayedEvent = @"UPDATE PlayerStats SET [BetrayedCircles] = [BetrayedCircles] + 1 WHERE [PlayerId] = @PlayerId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public CircleBetrayedPlayerStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(CircleBetrayedEvent circleBetrayedEvent) {
			return _viewContext.DbConnection.ExecuteAsync(UpdatePlayerStatsFromCircleBetrayedEvent, circleBetrayedEvent);
		}
	}
}
