using System;
using System.Collections.Generic;

namespace AndrewLarsson.Common.Domain {
	public abstract class AggregateRoot {
		public Guid Id { get; protected set; }

		private readonly List<DomainEvent> _events = new List<DomainEvent>();
		public IEnumerable<DomainEvent> Events {
			get { return _events; }
		}

		public void RaiseEvent(DomainEvent domainEvent) {
			_events.Add(domainEvent);
		}
	}
}
