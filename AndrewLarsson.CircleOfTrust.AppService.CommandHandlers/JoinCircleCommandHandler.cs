﻿using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.AppService.Commands;
using AndrewLarsson.CircleOfTrust.AppService.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using AndrewLarsson.CircleOfTrust.Domain.Rules;
using AndrewLarsson.CircleOfTrust.Domain.Services;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.CommandHandlers {
	public class JoinCircleCommandHandler : ICommandHandler<JoinCircleCommand> {
		private readonly IAggregateRootStore<Member> _memberStore;
		private readonly IAggregateRootStore<Player> _playerStore;
		private readonly IAggregateRootStore<Circle> _circleStore;
		private readonly ICircleRepository _circleRepository;
		private readonly IBetrayedCircleRepository _betrayedCircleRepository;
		private readonly IMemberRepository _memberRepository;

		public JoinCircleCommandHandler(
			IAggregateRootStore<Member> memberStore,
			IAggregateRootStore<Player> playerStore,
			IAggregateRootStore<Circle> circleStore,
			ICircleRepository circleRepository,
			IBetrayedCircleRepository betrayedCircleRepository,
			IMemberRepository memberRepository
		) {
			_memberStore = memberStore;
			_playerStore = playerStore;
			_circleStore = circleStore;
			_circleRepository = circleRepository;
			_betrayedCircleRepository = betrayedCircleRepository;
			_memberRepository = memberRepository;
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
			Member member = await JoinCircleService.JoinCircle(
				command.MemberId,
				command.PlayerId,
				command.CircleId,
				command.Key,
				new CircleKeyMustBeValidInOrderToJoinOrBetrayCircleRule(_circleRepository),
				new PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedRule(_betrayedCircleRepository),
				new PlayersMayNotJoinOrBetrayTheirOwnCircleRule(_circleRepository),
				new PlayersMayOnlyJoinACircleOnceRule(_memberRepository)
			);
			await _memberStore.SaveAsync(member);
		}
	}
}