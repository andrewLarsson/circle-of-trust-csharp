using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class MemberJoinedCircleStatsEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string UpdateCircleStatsFromMemberJoinedEvent = @"UPDATE CircleStats SET [Members] = [Members] + 1 WHERE [CircleId] = @CircleId;";
		private readonly IDbConnection _dbConnection;

		public MemberJoinedCircleStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			return _dbConnection.ExecuteAsync(UpdateCircleStatsFromMemberJoinedEvent, memberJoinedEvent);
		}
	}
}
