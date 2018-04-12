using System;
using System.Collections.Generic;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.Common.Host.Providers {
	public interface IEventHandlerProvider {
		IEnumerable<IEventHandler<TEvent>> GetEventHandlers<TEvent>() where TEvent : DomainEvent;
		IEnumerable<object> GetEventHandlers(Type domainEventType);
	}
}
