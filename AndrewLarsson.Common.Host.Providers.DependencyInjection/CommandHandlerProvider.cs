using System;
using AndrewLarsson.Common.AppService;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.Common.Host.Providers.DependencyInjection {
	public class CommandHandlerProvider : ICommandHandlerProvider {
		private static readonly Type CommandHandlerOpenType = typeof(ICommandHandler<>);
		private readonly IServiceProvider _serviceProvider;

		public CommandHandlerProvider(IServiceProvider serviceProvider) {
			_serviceProvider = serviceProvider;
		}

		public ICommandHandler<TCommand> GetCommandHandler<TCommand>() where TCommand : ICommand {
			return _serviceProvider.GetService<ICommandHandler<TCommand>>();
		}

		public object GetCommandHandler(Type commandType) {
			Type handlerType = CommandHandlerOpenType.MakeGenericType(commandType);
			return _serviceProvider.GetService(handlerType);
		}
	}
}
