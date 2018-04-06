using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.AggregateRoots {
	public class Member : AggregateRoot {
		public Guid PlayerId { get; }
		public Guid CircleId { get; }

		public Member(Guid id, Guid playerId, Guid circleId) {
			Id = id;
			PlayerId = playerId;
			CircleId = circleId;
		}
	}
}
