using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;

namespace AndrewLarsson.CircleOfTrust.Domain.Repositories {
	public interface IMemberRepository {
		Task<Member> FindMember(Guid memberId);
		Task<Member> FindMemberByCircleIdAndPlayerId(Guid circleId, Guid playerId);
	}
}
