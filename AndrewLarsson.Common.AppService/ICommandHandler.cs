using System.Threading.Tasks;

namespace AndrewLarsson.Common.AppService {
	public interface ICommandHandler<in TCommand> where TCommand : ICommand {
		Task HandleAsync(TCommand command);
	}
}
