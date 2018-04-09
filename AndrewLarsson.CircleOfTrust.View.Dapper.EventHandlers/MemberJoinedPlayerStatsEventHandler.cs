using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class MemberJoinedPlayerStatsEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string UpdatePlayerStatsFromMemberJoinedEvent = @"UPDATE PlayerStats SET [MemberOfCircles] = [MemberOfCircles] + 1 WHERE [PlayerId] = @PlayerId;";
		private readonly IDbConnection _dbConnection;

		public MemberJoinedPlayerStatsEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			return _dbConnection.ExecuteAsync(UpdatePlayerStatsFromMemberJoinedEvent, memberJoinedEvent);
		}
	}
}
