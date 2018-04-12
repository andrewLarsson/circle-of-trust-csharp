using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.AppService.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using AndrewLarsson.CircleOfTrust.Domain.Rules;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class BetrayCircleCommandHandler : ICommandHandler<BetrayCircleCommand> {
		private readonly IEventPublisher _eventPublisher;
		private readonly IAggregateRootStore<BetrayedCircle> _betrayedCircleStore;
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly ICircleRepository _circleRepository;
		private readonly IBetrayedCircleRepository _betrayedCircleRepository;
		private readonly IMemberRepository _memberRepository;

		public BetrayCircleCommandHandler(
			IEventPublisher eventPublisher,
			IAggregateRootStore<BetrayedCircle> betrayedCircleStore,
			IAggregateRootStore<Player> playerStore,
			IAggregateRootStore<Circle> circleStore,
			ICircleRepository circleRepository,
			IBetrayedCircleRepository betrayedCircleRepository,
			IMemberRepository memberRepository
		) {
			_eventPublisher = eventPublisher;
			_betrayedCircleStore = betrayedCircleStore;
			_playerStore = playerStore;
			_circleStore = circleStore;
			_circleRepository = circleRepository;
			_betrayedCircleRepository = betrayedCircleRepository;
			_memberRepository = memberRepository;
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
			BetrayedCircle betrayedCircle = await BetrayCircleService.BetrayCircle(
				command.BetrayedCircleId,
				command.CircleId,
				command.PlayerId,
				command.Key,
				new CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule(_circleRepository),
				new PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule(_betrayedCircleRepository),
				new PlayersMayNotJoinOrBetrayTheirOwnCircleRule(_circleRepository),
				new PlayersMayNotBetrayCircleTheyAreAMemberOfRule(_memberRepository)
			);
			await _betrayedCircleStore.SaveAsync(betrayedCircle);
			await _eventPublisher.PublishAsync(betrayedCircle.Events);
		}
	}
}
