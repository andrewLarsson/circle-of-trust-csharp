using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule {
		private readonly ICircleRepository _circleRepository;

		public CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule(ICircleRepository circleRepository) {
			_circleRepository = circleRepository;
		}

		public async Task Verify(Guid circleId, string key) {
			Circle circle = await _circleRepository.FindCircle(circleId);
			if (circle.Key != key) {
				throw new CircleKeyMustBeValidInOrderToJoinOrBetrayCircleException();
			}
		}
	}
}
