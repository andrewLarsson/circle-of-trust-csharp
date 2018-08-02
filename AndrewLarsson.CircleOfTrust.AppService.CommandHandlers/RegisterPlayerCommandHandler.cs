using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class RegisterPlayerCommandHandler : ICommandHandler<RegisterPlayerCommand> {
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly RegisterPlayerService _registerPlayerService;

		public RegisterPlayerCommandHandler(
			IAggregateRootStore<Player> playerStore,
			RegisterPlayerService registerPlayerService
		) {
			_playerStore = playerStore;
			_registerPlayerService = registerPlayerService;
		}

		public async Task HandleAsync(RegisterPlayerCommand command) {
			Player player = await _registerPlayerService.RegisterPlayer(
				command.PlayerId,
				command.Username,
				command.Password
			);
			await _playerStore.SaveAsync(player);
		}
	}
}
