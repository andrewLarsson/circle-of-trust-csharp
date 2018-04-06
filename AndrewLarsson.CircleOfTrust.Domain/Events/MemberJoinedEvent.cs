using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.Events {
	public class MemberJoinedEvent : DomainEvent {
		public Guid PlayerId { get; }
		public Guid CircleId { get; }

		public MemberJoinedEvent(Guid aggregateRootId, Guid playerId, Guid circleId)
			: base(aggregateRootId) {
			PlayerId = playerId;
			CircleId = circleId;
		}
	}
}
