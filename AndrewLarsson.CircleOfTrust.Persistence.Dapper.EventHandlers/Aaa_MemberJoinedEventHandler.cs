using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Host;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class Aaa_MemberJoinedEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string InsertMemberFromMemberJoinedEvent = @"INSERT INTO Member ([Id], [PlayerId], [CircleId]) VALUES (@AggregateRootId, @PlayerId, @CircleId);";
		private readonly IDbConnection _dbConnection;

		public Aaa_MemberJoinedEventHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			return _dbConnection.ExecuteAsync(InsertMemberFromMemberJoinedEvent, memberJoinedEvent);
		}
	}
}
