using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.Infrastructure {
	public class VolitileMemoryMemberStore : IAggregateRootStore<Member> {
		private readonly Dictionary<Guid, Member> _members = new Dictionary<Guid, Member>();

		public Task<Member> LoadAsync(Guid id) {
			if (!_members.ContainsKey(id)) {
				throw new Exception("Member does not exist.");
			}
			return Task.FromResult(_members[id]);
		}

		public Task SaveAsync(Member member) {
			_members[member.Id] = member;
			return Task.FromResult<object>(null);
		}
	}
}
