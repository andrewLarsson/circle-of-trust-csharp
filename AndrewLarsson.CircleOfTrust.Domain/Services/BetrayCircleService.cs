using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class BetrayCircleService {
		private readonly CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule _circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule;
		private readonly PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule _playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule;
		private readonly PlayersMayNotJoinOrBetrayTheirOwnCircleRule _playersMayNotJoinOrBetrayTheirOwnCircleRule;
		private readonly PlayersMayNotBetrayCircleTheyAreAMemberOfRule _playersMayNotBetrayCircleTheyAreAMemberOfRule;

		public BetrayCircleService(
			CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule,
			PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule,
			PlayersMayNotJoinOrBetrayTheirOwnCircleRule playersMayNotJoinOrBetrayTheirOwnCircleRule,
			PlayersMayNotBetrayCircleTheyAreAMemberOfRule playersMayNotBetrayCircleTheyAreAMemberOfRule
		) {
			_circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule = circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule;
			_playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule = playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule;
			_playersMayNotJoinOrBetrayTheirOwnCircleRule = playersMayNotJoinOrBetrayTheirOwnCircleRule;
			_playersMayNotBetrayCircleTheyAreAMemberOfRule = playersMayNotBetrayCircleTheyAreAMemberOfRule;
		}

		public async Task<BetrayedCircle> BetrayCircle(
			Guid betrayedCircleId,
			Guid circleId,
			Guid playerId,
			string key
		) {
			await _circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule.Verify(circleId, key);
			await _playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule.Verify(circleId);
			await _playersMayNotJoinOrBetrayTheirOwnCircleRule.Verify(circleId, playerId);
			await _playersMayNotBetrayCircleTheyAreAMemberOfRule.Verify(circleId, playerId);
			BetrayedCircle betrayedCircle = new BetrayedCircle(betrayedCircleId, circleId, playerId);
			betrayedCircle.RaiseEvent(new CircleBetrayedEvent(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId));
			return betrayedCircle;
		}
	}
}
