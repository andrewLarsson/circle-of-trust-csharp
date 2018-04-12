using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperCircleStore : IAggregateRootStore<Circle> {
		private static readonly string LoadCircle = @"SELECT * FROM Circle WHERE Id = @Id;";
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public DapperCircleStore(CircleOfTrustDapperPersistenceContext persistenceContext) {
			_persistenceContext = persistenceContext;
		}

		public async Task<Circle> LoadAsync(Guid id) {
			Models.Circle circle = await _persistenceContext.DbConnection.QueryFirstOrDefaultAsync<Models.Circle>(LoadCircle, new { Id = id });
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
