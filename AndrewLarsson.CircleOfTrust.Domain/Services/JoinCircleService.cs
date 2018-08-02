using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class JoinCircleService {
		private readonly CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule _circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule;
		private readonly PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule _playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule;
		private readonly PlayersMayNotJoinOrBetrayTheirOwnCircleRule _playersMayNotJoinOrBetrayTheirOwnCircleRule;
		private readonly PlayersMayOnlyJoinACircleOnceRule _playersMayOnlyJoinACircleOnceRule;

		public JoinCircleService(
			CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule,
			PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule,
			PlayersMayNotJoinOrBetrayTheirOwnCircleRule playersMayNotJoinOrBetrayTheirOwnCircleRule,
			PlayersMayOnlyJoinACircleOnceRule playersMayOnlyJoinACircleOnceRule
		) {
			_circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule = circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule;
			_playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule = playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule;
			_playersMayNotJoinOrBetrayTheirOwnCircleRule = playersMayNotJoinOrBetrayTheirOwnCircleRule;
			_playersMayOnlyJoinACircleOnceRule = playersMayOnlyJoinACircleOnceRule;
		}

		public async Task<Member> JoinCircle(
			Guid memberId,
			Guid playerId,
			Guid circleId,
			string key
		) {
			await _circleKeyMustBeValidInOrderToJoinOrBetrayCircleRule.Verify(circleId, key);
			await _playersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule.Verify(circleId);
			await _playersMayNotJoinOrBetrayTheirOwnCircleRule.Verify(circleId, playerId);
			await _playersMayOnlyJoinACircleOnceRule.Verify(circleId, playerId);
			Member member = new Member(memberId, playerId, circleId);
			member.RaiseEvent(new MemberJoinedEvent(member.Id, member.PlayerId, member.CircleId));
			return member;
		}
	}
}
