using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.EventHandlers {
	public class Aaa_MemberJoinedEventHandler : IEventHandler<MemberJoinedEvent> {
		private static readonly string InsertMemberFromMemberJoinedEvent = @"INSERT INTO Member ([Id], [PlayerId], [CircleId]) VALUES (@AggregateRootId, @PlayerId, @CircleId);";
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public Aaa_MemberJoinedEventHandler(CircleOfTrustDapperPersistenceContext persistenceContext) {
			_persistenceContext = persistenceContext;
		}

		public Task HandleAsync(MemberJoinedEvent memberJoinedEvent) {
			return _persistenceContext.DbConnection.ExecuteAsync(InsertMemberFromMemberJoinedEvent, memberJoinedEvent);
		}
	}
}
