using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class PlayersMayOnlyJoinACircleOnceRule {
		private readonly IMemberRepository _memberRepository;

		public PlayersMayOnlyJoinACircleOnceRule(IMemberRepository memberRepository) {
			_memberRepository = memberRepository;
		}

		public async Task Verify(Guid circleId, Guid playerId) {
			Member member = await _memberRepository.FindMemberByCircleIdAndPlayerId(circleId, playerId);
			if (member != null) {
				throw new PlayersMayOnlyJoinACircleOnceException();
			}
		}
	}
}