using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperBetrayedCircleStore : IAggregateRootStore<BetrayedCircle> {
		private static readonly string LoadBetrayedCircle = @"SELECT * FROM BetrayedCircle WHERE Id = @Id;";
		private readonly IDbConnection _dbConnection;

		public DapperBetrayedCircleStore(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<BetrayedCircle> LoadAsync(Guid id) {
			Models.BetrayedCircle betrayedCircle = await _dbConnection.QueryFirstOrDefaultAsync<Models.BetrayedCircle>(LoadBetrayedCircle, new { Id = id });
			if (betrayedCircle == null) {
				return null;
			}
			return new BetrayedCircle(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId);
		}

		public Task SaveAsync(BetrayedCircle betrayedCircle) {
			return Task.FromResult<object>(null);
		}
	}
}
