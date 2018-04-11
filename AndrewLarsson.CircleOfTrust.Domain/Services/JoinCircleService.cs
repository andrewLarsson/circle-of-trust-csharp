using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class JoinCircleService {
		public static async Task<Member> JoinCircle(
			Guid memberId,
			Guid playerId,
			Guid circleId,
			string key,
			CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule,
			PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule,
			PlayersMayNotJoinOrBetrayTheirOwnCircleRule playersMayNotJoinOrBetrayTheirOwnCircleRule,
			PlayersMayOnlyJoinACircleOnceRule playersMayOnlyJoinACircleOnceRule
		) {
			await circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule.Verify(circleId, key);
			await playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule.Verify(circleId);
			await playersMayNotJoinOrBetrayTheirOwnCircleRule.Verify(circleId, playerId);
			await playersMayOnlyJoinACircleOnceRule.Verify(circleId, playerId);
			Member member = new Member(memberId, playerId, circleId);
			member.RaiseEvent(new MemberJoinedEvent(member.Id, member.PlayerId, member.CircleId));
			return member;
		}
	}
}
