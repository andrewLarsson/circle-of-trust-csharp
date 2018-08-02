using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.AppService.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class InitiateCircleCommandHandler : ICommandHandler<InitiateCircleCommand> {
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly InitiateCircleService _initiateCircleService;

		public InitiateCircleCommandHandler(
			IAggregateRootStore<Circle> circleStore,
			IAggregateRootStore<Player> playerStore,
			InitiateCircleService initiateCircleService
		) {
			_circleStore = circleStore;
			_playerStore = playerStore;
			_initiateCircleService = initiateCircleService;
		}

		public async Task HandleAsync(InitiateCircleCommand command) {
			Player player = await _playerStore.LoadAsync(command.PlayerId);
			if (player == null) {
				throw new PlayerDoesNotExistException();
			}
			Circle circle = await _initiateCircleService.InitiateCircle(
				command.CircleId,
				command.PlayerId,
				command.Name,
				command.Key
			);
			await _circleStore.SaveAsync(circle);
		}
	}
}
