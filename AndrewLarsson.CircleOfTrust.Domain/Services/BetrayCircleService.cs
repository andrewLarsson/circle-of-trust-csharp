using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class BetrayCircleService {
		public static async Task<BetrayedCircle> BetrayCircle(
			Guid betrayedCircleId,
			Guid circleId,
			Guid playerId,
			string key,
			CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule,
			PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule,
			PlayersMayNotJoinOrBetrayTheirOwnCircleRule playersMayNotJoinOrBetrayTheirOwnCircleRule,
			PlayersMayNotBetrayCircleTheyAreAMemberOfRule playersMayNotBetrayCircleTheyAreAMemberOfRule
		) {
			await circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule.Verify(circleId, key);
			await playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule.Verify(circleId);
			await playersMayNotJoinOrBetrayTheirOwnCircleRule.Verify(circleId, playerId);
			await playersMayNotBetrayCircleTheyAreAMemberOfRule.Verify(circleId, playerId);
			BetrayedCircle betrayedCircle = new BetrayedCircle(betrayedCircleId, circleId, playerId);
			betrayedCircle.RaiseEvent(new CircleBetrayedEvent(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId));
			return betrayedCircle;
		}
	}
}
