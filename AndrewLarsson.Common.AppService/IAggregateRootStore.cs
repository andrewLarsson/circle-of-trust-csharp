using System;
using System.Threading.Tasks;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.Common.AppService {
	public interface IAggregateRootStore<T> where T : AggregateRoot {
		Task<T> LoadAsync(Guid id);
		Task SaveAsync(T t);
	}
}
