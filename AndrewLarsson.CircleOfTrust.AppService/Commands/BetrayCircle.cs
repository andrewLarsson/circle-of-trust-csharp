using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class BetrayCircleCommand : ICommand {
		public Guid BetrayedCircleId { get; set; }
		public Guid CircleId { get; set; }
		public Guid PlayerId { get; set; }
		public string Key { get; set; }
	}

	public class BetrayCircleHandler : ICommandHandler<BetrayCircleCommand> {
		private readonly IEventPublisher _eventPublisher;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly IAggregateRootStore<BetrayedCircle> _betrayedCircleStore;

		public BetrayCircleHandler(
			IEventPublisher eventPublisher,
			IAggregateRootStore<Circle> circleStore,
			IAggregateRootStore<BetrayedCircle> betrayedCircleStore
		) {
			_eventPublisher = eventPublisher;
			_circleStore = circleStore;
			_betrayedCircleStore = betrayedCircleStore;
		}

		public async Task HandleAsync(BetrayCircleCommand command) {
			Circle circle = await _circleStore.LoadAsync(command.CircleId);
			BetrayedCircle betrayedCircle = BetrayCircleService.BetrayCircle(
				command.BetrayedCircleId,
				circle,
				command.PlayerId,
				command.Key
			);
			await _betrayedCircleStore.SaveAsync(betrayedCircle);
			await _eventPublisher.PublishAsync(betrayedCircle.Events);
		}
	}
}
