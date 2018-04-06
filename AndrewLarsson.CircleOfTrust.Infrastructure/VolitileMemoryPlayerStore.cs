using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.Infrastructure {
	public class VolitileMemoryPlayerStore : IAggregateRootStore<Player> {
		private readonly Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();

		public Task<Player> LoadAsync(Guid id) {
			if (!_players.ContainsKey(id)) {
				throw new Exception("Circle does not exist.");
			}
			return Task.FromResult(_players[id]);
		}

		public Task SaveAsync(Player player) {
			_players[player.Id] = player;
			return Task.FromResult<object>(null);
		}
	}
}
