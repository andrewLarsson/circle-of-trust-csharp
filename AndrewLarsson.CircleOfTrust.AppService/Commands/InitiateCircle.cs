using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
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

		public InitiateCircleHandler(
			IEventPublisher eventPublisher,
			IAggregateRootStore<Circle> circleStore
		) {
			_eventPublisher = eventPublisher;
			_circleStore = circleStore;
		}

		public async Task HandleAsync(InitiateCircleCommand command) {
			Circle circle = InitiateCircleService.InitiateCircle(
				command.CircleId,
				command.PlayerId,
				command.Name,
				command.Key
			);
			await _circleStore.SaveAsync(circle);
			await _eventPublisher.PublishAsync(circle.Events);
		}
	}
}
