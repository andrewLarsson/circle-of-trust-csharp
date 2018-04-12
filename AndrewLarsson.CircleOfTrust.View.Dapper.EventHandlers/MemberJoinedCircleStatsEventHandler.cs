using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class MemberJoinedCircleStatsEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string UpdateCircleStatsFromMemberJoinedEvent = @"UPDATE CircleStats SET [Members] = [Members] + 1 WHERE [CircleId] = @CircleId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public MemberJoinedCircleStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			return _viewContext.DbConnection.ExecuteAsync(UpdateCircleStatsFromMemberJoinedEvent, memberJoinedEvent);
		}
	}
}
