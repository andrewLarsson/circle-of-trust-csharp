using System.Threading.Tasks;

namespace AndrewLarsson.Common.Domain {
	public interface IEventHandler<TEvent> where TEvent : DomainEvent {
		Task HandleAsync(TEvent tEvent);
	}
}
