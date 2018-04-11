using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class PlayersMustHaveAUniqueUsernameRule {
		private readonly IPlayerRepository _playerRepository;

		public PlayersMustHaveAUniqueUsernameRule(IPlayerRepository playerRepository) {
			_playerRepository = playerRepository;
		}

		public async Task Verify(string username) {
			Player player = await _playerRepository.FindPlayerByUsername(username);
			if (player != null) {
				throw new PlayersMustHaveAUniqueUserameException();
			}
		}

		public async Task Verify(Guid playerId, string username) {
			Player player = await _playerRepository.FindPlayerByUsername(username);
			if (player != null && player.Id != playerId) {
				throw new PlayersMustHaveAUniqueUserameException();
			}
		}
	}
}
