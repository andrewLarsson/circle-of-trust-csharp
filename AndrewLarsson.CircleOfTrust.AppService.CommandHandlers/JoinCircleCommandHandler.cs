using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.AppService.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class JoinCircleCommandHandler : ICommandHandler<JoinCircleCommand> {
		private readonly IAggregateRootStore<Member> _memberStore;
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly JoinCircleService _joinCircleService;

		public JoinCircleCommandHandler(
			IAggregateRootStore<Member> memberStore,
			IAggregateRootStore<Player> playerStore,
			IAggregateRootStore<Circle> circleStore,
			JoinCircleService joinCircleService
		) {
			_memberStore = memberStore;
			_playerStore = playerStore;
			_circleStore = circleStore;
			_joinCircleService = joinCircleService;
		}

		public async Task HandleAsync(JoinCircleCommand command) {
			Player player = await _playerStore.LoadAsync(command.PlayerId);
			if (player == null) {
				throw new PlayerDoesNotExistException();
			}
			Circle circle = await _circleStore.LoadAsync(command.CircleId);
			if (circle == null) {
				throw new CircleDoesNotExistException();
			}
			Member member = await _joinCircleService.JoinCircle(
				command.MemberId,
				command.PlayerId,
				command.CircleId,
				command.Key
			);
			await _memberStore.SaveAsync(member);
		}
	}
}
