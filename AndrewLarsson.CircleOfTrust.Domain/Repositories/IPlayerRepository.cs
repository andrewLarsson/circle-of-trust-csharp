using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;

namespace AndrewLarsson.CircleOfTrust.Domain.Repositories {
	public interface IPlayerRepository {
		Task<Player> FindPlayer(Guid playerId);
		Task<Player> FindPlayerByUsername(string username);
	}
}
