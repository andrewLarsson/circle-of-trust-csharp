using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndrewLarsson.Common.AppService;
using AndrewLarsson.Common.Domain;
using AndrewLarsson.Common.Host;

namespace AndrewLarsson.CircleOfTrust.Infrastructure {
	public class InProcessEventPublisher : IEventPublisher {
		private readonly IEventHandlerProvider _eventHandlerProvider;

		public InProcessEventPublisher(IEventHandlerProvider eventHandlerProvider) {
			_eventHandlerProvider = eventHandlerProvider;
		}

		public async Task PublishAsync(IEnumerable<DomainEvent> events) {
			foreach (DomainEvent domainEvent in events) {
				Type eventType = domainEvent.GetType();
				List<object> eventHandlers = _eventHandlerProvider.GetEventHandlers(eventType).OrderBy(o => o.GetType().Name).ToList();
				foreach (dynamic eventHandler in eventHandlers) {
					await eventHandler.HandleAsync((dynamic)domainEvent);
				}
			}
		}
	}
}
