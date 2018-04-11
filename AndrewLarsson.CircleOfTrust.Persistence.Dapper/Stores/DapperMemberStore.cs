using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperMemberStore : IAggregateRootStore<Member> {
		private static readonly string LoadMember = @"SELECT * FROM Member WHERE Id = @Id;";
		private readonly IDbConnection _dbConnection;

		public DapperMemberStore(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<Member> LoadAsync(Guid id) {
			Models.Member member = await _dbConnection.QueryFirstOrDefaultAsync<Models.Member>(LoadMember, new { Id = id });
			if (member == null) {
				return null;
			}
			return new Member(member.Id, member.PlayerId, member.CircleId);
		}

		public Task SaveAsync(Member member) {
			return Task.FromResult<object>(null);
		}
	}
}
