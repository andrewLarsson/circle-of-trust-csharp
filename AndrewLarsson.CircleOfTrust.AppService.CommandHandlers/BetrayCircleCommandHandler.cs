using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.AppService.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class BetrayCircleCommandHandler : ICommandHandler<BetrayCircleCommand> {
		private readonly IAggregateRootStore<BetrayedCircle> _betrayedCircleStore;
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly BetrayCircleService _betrayCircleService;

		public BetrayCircleCommandHandler(
			IAggregateRootStore<BetrayedCircle> betrayedCircleStore,
			IAggregateRootStore<Player> playerStore,
			IAggregateRootStore<Circle> circleStore,
			BetrayCircleService betrayCircleService
		) {
			_betrayedCircleStore = betrayedCircleStore;
			_playerStore = playerStore;
			_circleStore = circleStore;
			_betrayCircleService = betrayCircleService;
		}

		public async Task HandleAsync(BetrayCircleCommand command) {
			Player player = await _playerStore.LoadAsync(command.PlayerId);
			if (player == null) {
				throw new PlayerDoesNotExistException();
			}
			Circle circle = await _circleStore.LoadAsync(command.CircleId);
			if (circle == null) {
				throw new CircleDoesNotExistException();
			}
			BetrayedCircle betrayedCircle = await _betrayCircleService.BetrayCircle(
				command.BetrayedCircleId,
				command.CircleId,
				command.PlayerId,
				command.Key
			);
			await _betrayedCircleStore.SaveAsync(betrayedCircle);
		}
	}
}
