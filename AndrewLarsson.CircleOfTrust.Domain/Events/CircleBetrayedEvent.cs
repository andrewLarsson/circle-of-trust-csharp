using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.Events {
	public class CircleBetrayedEvent : DomainEvent {
		public Guid CircleId { get; }
		public Guid PlayerId { get; }

		public CircleBetrayedEvent(Guid aggregateRootId, Guid circleId, Guid playerId)
			: base(aggregateRootId) {
			CircleId = circleId;
			PlayerId = playerId;
		}
	}
}
