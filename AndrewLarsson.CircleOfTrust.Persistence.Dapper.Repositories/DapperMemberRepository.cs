using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Repositories {
	public class DapperMemberRepository : IMemberRepository {
		private static readonly string LoadMember = @"SELECT * FROM Member WHERE Id = @Id;";
		private static readonly string LoadMemberByCircleIdAndPlayerId = @"SELECT * FROM Member WHERE CircleId = @CircleId AND PlayerId = @PlayerId;";
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public DapperMemberRepository(CircleOfTrustDapperPersistenceContext persistenceContext) {
			_persistenceContext = persistenceContext;
		}

		public async Task<Member> FindMember(Guid memberId) {
			Models.Member member = await _persistenceContext.DbConnection.QueryFirstOrDefaultAsync<Models.Member>(LoadMember, new { Id = memberId });
			if (member == null) {
				return null;
			}
			return new Member(member.Id, member.PlayerId, member.CircleId);
		}

		public async Task<Member> FindMemberByCircleIdAndPlayerId(Guid circleId, Guid playerId) {
			Models.Member member = await _persistenceContext.DbConnection.QueryFirstOrDefaultAsync<Models.Member>(LoadMemberByCircleIdAndPlayerId, new { CircleId = circleId, PlayerId = playerId });
			if (member == null) {
				return null;
			}
			return new Member(member.Id, member.PlayerId, member.CircleId);
		}
	}
}
