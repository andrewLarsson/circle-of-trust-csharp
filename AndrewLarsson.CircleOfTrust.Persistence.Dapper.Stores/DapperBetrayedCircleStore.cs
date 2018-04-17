using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperBetrayedCircleStore : IAggregateRootStore<BetrayedCircle> {
		private static readonly string LoadBetrayedCircle = @"SELECT * FROM BetrayedCircle WHERE Id = @Id;";
		private readonly IEventPublisher _eventPublisher;
		private readonly CircleOfTrustDapperPersistenceContext _persistenceContext;

		public DapperBetrayedCircleStore(IEventPublisher eventPublisher, CircleOfTrustDapperPersistenceContext persistenceContext) {
			_eventPublisher = eventPublisher;
			_persistenceContext = persistenceContext;
		}

		public async Task<BetrayedCircle> LoadAsync(Guid id) {
			Models.BetrayedCircle betrayedCircle = await _persistenceContext.DbConnection.QueryFirstOrDefaultAsync<Models.BetrayedCircle>(LoadBetrayedCircle, new { Id = id });
			if (betrayedCircle == null) {
				return null;
			}
			return new BetrayedCircle(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId);
		}

		public Task SaveAsync(BetrayedCircle betrayedCircle) {
			return _eventPublisher.PublishAsync(betrayedCircle.FlushEvents());
		}
	}
}
