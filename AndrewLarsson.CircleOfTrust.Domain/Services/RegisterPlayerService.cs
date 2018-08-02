using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class RegisterPlayerService {
		private readonly PlayersMustHaveAUniqueUsernameRule _playersMustHaveAUniqueUsernameRule;

		public RegisterPlayerService(
			PlayersMustHaveAUniqueUsernameRule playersMustHaveAUniqueUsernameRule
		) {
			_playersMustHaveAUniqueUsernameRule = playersMustHaveAUniqueUsernameRule;
		}

		public async Task<Player> RegisterPlayer(
			Guid playerId,
			string username,
			string password
		) {
			await _playersMustHaveAUniqueUsernameRule.Verify(username);
			Player player = new Player(playerId, username, password);
			player.RaiseEvent(new PlayerRegisteredEvent(player.Id, player.Username, player.Password));
			return player;
		}
	}
}
