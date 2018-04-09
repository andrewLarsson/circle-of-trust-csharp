using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class Zzz_MemberJoinedCircleLeaderboardContenderEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string LoadCircleStats = @"SELECT * FROM CircleStats WHERE [CircleId] = @CircleId;";
		private static readonly string LoadLowestCircleLeaderboardContender = @"SELECT TOP(1) * FROM CircleLeaderboardContender ORDER BY [Members] ASC;";
		private static readonly string LoadCircleLeaderboardContenderByCircleId = @"SELECT * FROM CircleLeaderboardContender WHERE [CircleId] = @CircleId;";
		private static readonly string UpdateCircleLeaderboardContender = @"UPDATE CircleLeaderboardContender SET [CircleId] = @CircleId, [Members] = @Members WHERE [CircleLeaderboardContenderId] = @CircleLeaderboardContenderId;";
		private readonly IDbConnection _dbConnection;

		public Zzz_MemberJoinedCircleLeaderboardContenderEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			CircleStats circleStats = await _dbConnection.QuerySingleAsync<CircleStats>(LoadCircleStats, new {
				CircleId = memberJoinedEvent.CircleId
			});
			CircleLeaderboardContender circleLeaderboardContender = await _dbConnection.QuerySingleAsync<CircleLeaderboardContender>(LoadLowestCircleLeaderboardContender);
			if (circleStats.Members > circleLeaderboardContender.Members) {
				circleLeaderboardContender = (await _dbConnection.QuerySingleOrDefaultAsync<CircleLeaderboardContender>(LoadCircleLeaderboardContenderByCircleId, circleStats)) ?? circleLeaderboardContender;
				await _dbConnection.ExecuteAsync(UpdateCircleLeaderboardContender, new CircleLeaderboardContender {
					CircleLeaderboardContenderId = circleLeaderboardContender.CircleLeaderboardContenderId,
					CircleId = circleStats.CircleId,
					Members = circleStats.Members
				});
			}
		}
	}
}
