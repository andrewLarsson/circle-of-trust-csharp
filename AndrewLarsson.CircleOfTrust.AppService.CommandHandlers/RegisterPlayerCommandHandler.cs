using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using AndrewLarsson.CircleOfTrust.Domain.Rules;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class RegisterPlayerCommandHandler : ICommandHandler<RegisterPlayerCommand> {
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly IPlayerRepository _playerRepository;

		public RegisterPlayerCommandHandler(
			IAggregateRootStore<Player> playerStore,
			IPlayerRepository playerRepository
		) {
			_playerStore = playerStore;
			_playerRepository = playerRepository;
		}

		public async Task HandleAsync(RegisterPlayerCommand command) {
			Player player = await RegisterPlayerService.RegisterPlayer(
				command.PlayerId,
				command.Username,
				command.Password,
				new PlayersMustHaveAUniqueUsernameRule(_playerRepository)
			);
			await _playerStore.SaveAsync(player);
		}
	}
}
