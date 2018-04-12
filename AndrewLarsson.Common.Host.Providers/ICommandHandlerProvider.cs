using System;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.Common.Host.Providers {
	public interface ICommandHandlerProvider {
		ICommandHandler<TCommand> GetCommandHandler<TCommand>() where TCommand : ICommand;
		object GetCommandHandler(Type commandType);
	}
}
