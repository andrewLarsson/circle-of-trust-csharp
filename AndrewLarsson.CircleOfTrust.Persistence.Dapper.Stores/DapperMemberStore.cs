using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperMemberStore : IAggregateRootStore<Member> {
		private static readonly string LoadMember = @"SELECT * FROM Member WHERE Id = @Id;";
		private readonly IEventPublisher _eventPublisher;
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public DapperMemberStore(
			IEventPublisher eventPublisher,
			CircleOfTrustDapperPersistenceContext persistenceContext
		) {
			_eventPublisher = eventPublisher;
			_persistenceContext = persistenceContext;
		}

		public async Task<Member> LoadAsync(Guid id) {
			Models.Member member = await _persistenceContext.DbConnection.QueryFirstOrDefaultAsync<Models.Member>(LoadMember, new { Id = id });
			if (member == null) {
				return null;
			}
			return new Member(member.Id, member.PlayerId, member.CircleId);
		}

		public Task SaveAsync(Member member) {
			return _eventPublisher.PublishAsync(member.FlushEvents());
		}
	}
}
