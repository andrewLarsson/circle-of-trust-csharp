using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using AndrewLarsson.CircleOfTrust.Domain.Rules;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class InitiateCircleCommand : ICommand {
		public Guid CircleId { get; set; }
		public Guid PlayerId { get; set; }
		public string Name { get; set; }
		public string Key { get; set; }
	}

	public class InitiateCircleHandler : ICommandHandler<InitiateCircleCommand> {
		private readonly IEventPublisher _eventPublisher;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly ICircleRepository _circleRepository;

		public InitiateCircleHandler(
			IEventPublisher eventPublisher,
			IAggregateRootStore<Circle> circleStore,
			IAggregateRootStore<Player> playerStore,
			ICircleRepository circleRepository
		) {
			_eventPublisher = eventPublisher;
			_circleStore = circleStore;
			_playerStore = playerStore;
			_circleRepository = circleRepository;
		}

		public async Task HandleAsync(InitiateCircleCommand command) {
			Player player = await _playerStore.LoadAsync(command.PlayerId);
			if (player == null) {
				throw new PlayerDoesNotExistException();
			}
			Circle circle = await InitiateCircleService.InitiateCircle(
				command.CircleId,
				command.PlayerId,
				command.Name,
				command.Key,
				new CirclesMustHaveAUniqueNameRule(_circleRepository),
				new PlayersMayOnlyInitiateOneCircleRule(_circleRepository)
			);
			await _circleStore.SaveAsync(circle);
			await _eventPublisher.PublishAsync(circle.Events);
		}
	}
}
