using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrewLarsson.Common.Domain {
	public abstract class AggregateRoot {
		public Guid Id { get; protected set; }

		private readonly List<DomainEvent> _events = new List<DomainEvent>();

		public void RaiseEvent(DomainEvent domainEvent) {
			_events.Add(domainEvent);
		}

		public IEnumerable<DomainEvent> FlushEvents() {
			IEnumerable<DomainEvent> events = _events.ToList();
			_events.Clear();
			return events;
		}
	}
}
