using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.Common.AppService;
using AndrewLarsson.Common.Host;
using EdjCase.JsonRpc.Router;

namespace AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc.Controllers {
	[RpcRoute("api/rpc.json")]
	public class CircleController : RpcController {
		private readonly ICommandHandlerProvider _commandHandlerProvider;
		private readonly IPlayerAuthenticationService _playerAuthenticationService;

		public CircleController(ICommandHandlerProvider commandHandlerProvider, IPlayerAuthenticationService playerAuthenticationService) {
			_commandHandlerProvider = commandHandlerProvider;
			_playerAuthenticationService = playerAuthenticationService;
		}

		public async Task<Guid> InitiateCircle(InitiateCircleCommand command, PlayerAuthentication authentication) {
			command.CircleId = Guid.NewGuid();
			await _playerAuthenticationService.AuthenticateAndAuthorizePlayer(authentication, command.PlayerId);
			ICommandHandler<InitiateCircleCommand> handler = _commandHandlerProvider.GetCommandHandler<InitiateCircleCommand>();
			await handler.HandleAsync(command);
			return command.CircleId;
		}

		public async Task<Guid> JoinCircle(JoinCircleCommand command, PlayerAuthentication authentication) {
			command.MemberId = Guid.NewGuid();
			await _playerAuthenticationService.AuthenticateAndAuthorizePlayer(authentication, command.PlayerId);
			ICommandHandler<JoinCircleCommand> handler = _commandHandlerProvider.GetCommandHandler<JoinCircleCommand>();
			await handler.HandleAsync(command);
			return command.MemberId;
		}

		public async Task BetrayCircle(BetrayCircleCommand command, PlayerAuthentication authentication) {
			command.BetrayedCircleId = Guid.NewGuid();
			await _playerAuthenticationService.AuthenticateAndAuthorizePlayer(authentication, command.PlayerId);
			ICommandHandler<BetrayCircleCommand> handler = _commandHandlerProvider.GetCommandHandler<BetrayCircleCommand>();
			await handler.HandleAsync(command);
		}
	}
}
