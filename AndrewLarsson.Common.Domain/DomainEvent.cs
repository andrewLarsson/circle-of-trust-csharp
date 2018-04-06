using System;

namespace AndrewLarsson.Common.Domain {
	public abstract class DomainEvent {
		public Guid Id { get; }
		public Guid AggregateRootId { get; }
		public object EventContext { get; }

		protected DomainEvent(Guid aggregateRootId, object eventContext) {
			Id = Guid.NewGuid();
			AggregateRootId = aggregateRootId;
			EventContext = eventContext;
		}

		protected DomainEvent(Guid aggregateRootId) {
			Id = Guid.NewGuid();
			AggregateRootId = aggregateRootId;
			EventContext = null;
		}
	}
}
