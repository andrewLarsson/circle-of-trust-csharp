using System;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class RegisterPlayerService {
		public static Player RegisterPlayer(Guid playerId, string username, string password) {
			Player player = new Player(playerId, username, password);
			player.RaiseEvent(new PlayerRegisteredEvent(player.Id, player.Username, player.Password));
			return player;
		}
	}
}
