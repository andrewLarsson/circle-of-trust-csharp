using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class MemberJoinedPlayerStatsEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string UpdatePlayerStatsFromMemberJoinedEvent = @"UPDATE PlayerStats SET [MemberOfCircles] = [MemberOfCircles] + 1 WHERE [PlayerId] = @PlayerId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public MemberJoinedPlayerStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			return _viewContext.DbConnection.ExecuteAsync(UpdatePlayerStatsFromMemberJoinedEvent, memberJoinedEvent);
		}
	}
}
