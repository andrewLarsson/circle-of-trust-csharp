using System.Threading.Tasks;

namespace AndrewLarsson.Common.AppService {
	public interface ICommandHandler<in T> where T : ICommand {
		Task HandleAsync(T command);
	}
}
