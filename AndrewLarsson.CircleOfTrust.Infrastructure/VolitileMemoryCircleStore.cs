using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.Infrastructure {
	public class VolitileMemoryCircleStore : IAggregateRootStore<Circle> {
		private readonly Dictionary<Guid, Circle> _circles = new Dictionary<Guid, Circle>();

		public Task<Circle> LoadAsync(Guid id) {
			if (!_circles.ContainsKey(id)) {
				throw new Exception("Circle does not exist.");
			}
			return Task.FromResult(_circles[id]);
		}

		public Task SaveAsync(Circle circle) {
			_circles[circle.Id] = circle;
			return Task.FromResult<object>(null);
		}
	}
}
