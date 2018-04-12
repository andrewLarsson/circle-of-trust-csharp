using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class Zzz_MemberJoinedCircleLeaderboardContenderEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string LoadCircleStats = @"SELECT * FROM CircleStats WHERE [CircleId] = @CircleId;";
		private static readonly string LoadLowestCircleLeaderboardContender = @"SELECT TOP(1) * FROM CircleLeaderboardContender ORDER BY [Members] ASC;";
		private static readonly string LoadCircleLeaderboardContenderByCircleId = @"SELECT * FROM CircleLeaderboardContender WHERE [CircleId] = @CircleId;";
		private static readonly string UpdateCircleLeaderboardContender = @"UPDATE CircleLeaderboardContender SET [CircleId] = @CircleId, [Members] = @Members WHERE [CircleLeaderboardContenderId] = @CircleLeaderboardContenderId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public Zzz_MemberJoinedCircleLeaderboardContenderEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			CircleLeaderboardContender circleLeaderboardContender = await _viewContext.DbConnection.QuerySingleOrDefaultAsync<CircleLeaderboardContender>(LoadLowestCircleLeaderboardContender);
			if (circleLeaderboardContender == null) {
				return;
			}
			CircleStats circleStats = await _viewContext.DbConnection.QuerySingleAsync<CircleStats>(LoadCircleStats, new {
				memberJoinedEvent.CircleId
			});
			if (circleStats.Members > circleLeaderboardContender.Members) {
				circleLeaderboardContender = (await _viewContext.DbConnection.QuerySingleOrDefaultAsync<CircleLeaderboardContender>(LoadCircleLeaderboardContenderByCircleId, circleStats)) ?? circleLeaderboardContender;
				await _viewContext.DbConnection.ExecuteAsync(UpdateCircleLeaderboardContender, new CircleLeaderboardContender {
					CircleLeaderboardContenderId = circleLeaderboardContender.CircleLeaderboardContenderId,
					CircleId = circleStats.CircleId,
					Members = circleStats.Members
				});
			}
		}
	}
}
