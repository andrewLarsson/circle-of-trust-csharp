using System.Threading.Tasks;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.Common.Host {
	public interface IEventHandler<TEvent> where TEvent : DomainEvent {
		Task HandleAsync(TEvent tEvent);
	}
}
