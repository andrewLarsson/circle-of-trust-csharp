using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class CircleBetrayedCircleStatsEventHandler : IEventHandler<CircleBetrayedEvent> {
		private static readonly string UpdateCircleStatsFromCircleBetrayedEvent = @"UPDATE CircleStats SET [IsBetrayed] = 1 WHERE [CircleId] = @CircleId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public CircleBetrayedCircleStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(CircleBetrayedEvent circleBetrayedEvent) {
			return _viewContext.DbConnection.ExecuteAsync(UpdateCircleStatsFromCircleBetrayedEvent, circleBetrayedEvent);
		}
	}
}
