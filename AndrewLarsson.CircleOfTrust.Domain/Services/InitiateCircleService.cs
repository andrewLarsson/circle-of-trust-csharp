using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class InitiateCircleService {
		private readonly CirclesMustHaveAUniqueNameRule _circlesMustHaveAUniqueNameRule;
		private readonly PlayersMayOnlyInitiateOneCircleRule _playersMayOnlyInitiateOneCircleRule;

		public InitiateCircleService(
			CirclesMustHaveAUniqueNameRule circlesMustHaveAUniqueNameRule,
			PlayersMayOnlyInitiateOneCircleRule playersMayOnlyInitiateOneCircleRule
		) {
			_circlesMustHaveAUniqueNameRule = circlesMustHaveAUniqueNameRule;
			_playersMayOnlyInitiateOneCircleRule = playersMayOnlyInitiateOneCircleRule;
		}

		public async Task<Circle> InitiateCircle(
			Guid circleId,
			Guid playerId,
			string name,
			string key
		) {
			await _circlesMustHaveAUniqueNameRule.Verify(name);
			await _playersMayOnlyInitiateOneCircleRule.Verify(playerId);
			Circle circle = new Circle(circleId, playerId, name, key);
			circle.RaiseEvent(new CircleInitiatedEvent(circle.Id, circle.PlayerId, circle.Name, circle.Key));
			return circle;
		}
	}
}
