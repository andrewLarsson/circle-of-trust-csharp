using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperCircleStore : IAggregateRootStore<Circle> {
		private static readonly string LoadCircle = @"SELECT * FROM Circle WHERE Id = @Id;";
		private readonly IDbConnection _dbConnection;

		public DapperCircleStore(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<Circle> LoadAsync(Guid id) {
			Models.Circle circle = await _dbConnection.QueryFirstOrDefaultAsync<Models.Circle>(LoadCircle, new { Id = id });
			if (circle == null) {
				return null;
			}
			return new Circle(circle.Id, circle.PlayerId, circle.Name, circle.Key);
		}

		public Task SaveAsync(Circle circle) {
			return Task.FromResult<object>(null);
		}
	}
}
