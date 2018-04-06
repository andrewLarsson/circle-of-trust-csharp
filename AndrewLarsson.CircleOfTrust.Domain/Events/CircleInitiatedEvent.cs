using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.Events {
	public class CircleInitiatedEvent : DomainEvent {
		public Guid PlayerId { get; }
		public string Name { get; }
		public string Key { get; }

		public CircleInitiatedEvent(Guid aggregateRootId, Guid playerId, string name, string key)
			: base(aggregateRootId) {
			PlayerId = playerId;
			Name = name;
			Key = key;
		}
	}
}
