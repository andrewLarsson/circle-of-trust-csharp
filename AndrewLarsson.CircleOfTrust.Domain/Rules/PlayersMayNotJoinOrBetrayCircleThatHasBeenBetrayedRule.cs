using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule {
		private readonly IBetrayedCircleRepository _betrayedCircleRepository;

		public PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule(IBetrayedCircleRepository betrayedCircleRepository) {
			_betrayedCircleRepository = betrayedCircleRepository;
		}

		public async Task Verify(Guid circleId) {
			BetrayedCircle betrayedCircle = await _betrayedCircleRepository.FindBetrayedCircleByCircleId(circleId);
			if (betrayedCircle != null) {
				throw new PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedException();
			}
		}
	}
}
