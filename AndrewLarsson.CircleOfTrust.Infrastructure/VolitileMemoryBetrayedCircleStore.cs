using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.Infrastructure {
	public class VolitileMemoryBetrayedCircleStore : IAggregateRootStore<BetrayedCircle> {
		private readonly Dictionary<Guid, BetrayedCircle> _betrayedCircles = new Dictionary<Guid, BetrayedCircle>();

		public Task<BetrayedCircle> LoadAsync(Guid id) {
			if (!_betrayedCircles.ContainsKey(id)) {
				throw new Exception("Betrayed Circle does not exist.");
			}
			return Task.FromResult(_betrayedCircles[id]);
		}

		public Task SaveAsync(BetrayedCircle betrayedCircle) {
			_betrayedCircles[betrayedCircle.Id] = betrayedCircle;
			return Task.FromResult<object>(null);
		}
	}
}
