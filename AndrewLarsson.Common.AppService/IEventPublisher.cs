using System.Collections.Generic;
using System.Threading.Tasks;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.Common.AppService {
	public interface IEventPublisher {
		Task PublishAsync(IEnumerable<DomainEvent> events);
	}
}
