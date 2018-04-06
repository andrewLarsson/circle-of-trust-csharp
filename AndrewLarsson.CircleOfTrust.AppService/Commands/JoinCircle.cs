using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class JoinCircleCommand : ICommand {
		public Guid MemberId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid CircleId { get; set; }
		public string Key { get; set; }
	}

	public class JoinCircleHandler : ICommandHandler<JoinCircleCommand> {
		private readonly IEventPublisher _eventPublisher;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly IAggregateRootStore<Member> _memberStore;

		public JoinCircleHandler(
			IEventPublisher eventPublisher,
			IAggregateRootStore<Circle> circleStore,
			IAggregateRootStore<Member> memberStore
		) {
			_eventPublisher = eventPublisher;
			_circleStore = circleStore;
			_memberStore = memberStore;
		}

		public async Task HandleAsync(JoinCircleCommand command) {
			Circle circle = await _circleStore.LoadAsync(command.CircleId);
			Member member = JoinCircleService.JoinCircle(
				command.MemberId,
				command.PlayerId,
				circle,
				command.Key
			);
			await _memberStore.SaveAsync(member);
			await _eventPublisher.PublishAsync(member.Events);
		}
	}
}
