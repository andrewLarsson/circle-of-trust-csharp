using System;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class JoinCircleService {
		public static Member JoinCircle(Guid memberId, Guid playerId, Circle circle, string key) {
			ValidateKeyService.ValidateKey(key, circle.Key);
			Member member = new Member(memberId, playerId, circle.Id);
			member.RaiseEvent(new MemberJoinedEvent(member.Id, member.PlayerId, member.CircleId));
			return member;
		}
	}
}
