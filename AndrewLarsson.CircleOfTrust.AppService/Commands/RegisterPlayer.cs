using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class RegisterPlayerCommand : ICommand {
		public Guid PlayerId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class RegisterPlayerHandler : ICommandHandler<RegisterPlayerCommand> {
		private readonly IEventPublisher _eventPublisher;
		private readonly IAggregateRootStore<Player> _playerStore;

		public RegisterPlayerHandler(
			IEventPublisher eventPublisher,
			IAggregateRootStore<Player> playerStore
		) {
			_eventPublisher = eventPublisher;
			_playerStore = playerStore;
		}

		public async Task HandleAsync(RegisterPlayerCommand command) {
			Player player = RegisterPlayerService.RegisterPlayer(
				command.PlayerId,
				command.Username,
				command.Password
			);
			await _playerStore.SaveAsync(player);
			await _eventPublisher.PublishAsync(player.Events);
		}
	}
}
