using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class PlayersMayNotJoinOrBetrayTheirOwnCircleRule {
		private readonly ICircleRepository _circleRepository;

		public PlayersMayNotJoinOrBetrayTheirOwnCircleRule(ICircleRepository circleRepository) {
			_circleRepository = circleRepository;
		}

		public async Task Verify(Guid circleId, Guid playerId) {
			Circle circle = await _circleRepository.FindCircle(circleId);
			if (circle.PlayerId == playerId) {
				throw new PlayersMayNotJoinOrBetrayTheirOwnCircleException();
			}
		}
	}
}
