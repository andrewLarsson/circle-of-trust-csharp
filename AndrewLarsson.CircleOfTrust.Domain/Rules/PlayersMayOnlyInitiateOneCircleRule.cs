using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class PlayersMayOnlyInitiateOneCircleRule {
		private readonly ICircleRepository _circleRepository;

		public PlayersMayOnlyInitiateOneCircleRule(ICircleRepository circleRepository) {
			_circleRepository = circleRepository;
		}

		public async Task Verify(Guid playerId) {
			Circle circle = await _circleRepository.FindCircleByPlayerId(playerId);
			if (circle != null) {
				throw new PlayersMayOnlyInitiateOneCircleException();
			}
		}
	}
}
