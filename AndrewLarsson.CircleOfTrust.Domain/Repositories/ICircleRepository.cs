using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;

namespace AndrewLarsson.CircleOfTrust.Domain.Repositories {
	public interface ICircleRepository {
		Task<Circle> FindCircle(Guid circleId);
		Task<Circle> FindCircleByName(string name);
		Task<Circle> FindCircleByPlayerId(Guid playerId);
	}
}
