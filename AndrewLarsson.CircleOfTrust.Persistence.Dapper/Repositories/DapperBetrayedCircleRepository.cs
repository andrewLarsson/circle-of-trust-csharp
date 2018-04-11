using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Repositories {
	public class DapperBetrayedCircleRepository : IBetrayedCircleRepository {
		private static readonly string LoadBetrayedCircle = @"SELECT * FROM BetrayedCircle WHERE Id = @Id;";
		private static readonly string LoadBetrayedCircleByCircleId = @"SELECT * FROM BetrayedCircle WHERE CircleId = @CircleId;";
		private readonly IDbConnection _dbConnection;

		public DapperBetrayedCircleRepository(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<BetrayedCircle> FindBetrayedCircle(Guid betrayedCircleId) {
			Models.BetrayedCircle betrayedCircle = await _dbConnection.QueryFirstOrDefaultAsync<Models.BetrayedCircle>(LoadBetrayedCircle, new { Id = betrayedCircleId });
			if (betrayedCircle == null) {
				return null;
			}
			return new BetrayedCircle(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId);
		}

		public async Task<BetrayedCircle> FindBetrayedCircleByCircleId(Guid circleId) {
			Models.BetrayedCircle betrayedCircle = await _dbConnection.QueryFirstOrDefaultAsync<Models.BetrayedCircle>(LoadBetrayedCircleByCircleId, new { CircleId = circleId });
			if (betrayedCircle == null) {
				return null;
			}
			return new BetrayedCircle(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId);
		}
	}
}
