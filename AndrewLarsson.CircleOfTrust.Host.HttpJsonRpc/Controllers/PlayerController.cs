using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.Common.AppService;
using AndrewLarsson.Common.Host;
using EdjCase.JsonRpc.Router;

namespace AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc.Controllers {
	[RpcRoute("api/rpc.json")]
	public class PlayerController : RpcController {
		private readonly ICommandHandlerProvider _commandHandlerProvider;
		private readonly IPlayerVerificationService _playerVerificationService;

		public PlayerController(ICommandHandlerProvider commandHandlerProvider, IPlayerVerificationService playerVerificationService) {
			_commandHandlerProvider = commandHandlerProvider;
			_playerVerificationService = playerVerificationService;
		}

		public async Task<Guid> RegisterPlayer(RegisterPlayerCommand command, PlayerVerification verification) {
			command.PlayerId = Guid.NewGuid();
			await _playerVerificationService.VerifyPlayer(verification);
			ICommandHandler<RegisterPlayerCommand> handler = _commandHandlerProvider.GetCommandHandler<RegisterPlayerCommand>();
			await handler.HandleAsync(command);
			return command.PlayerId;
		}
	}
}
