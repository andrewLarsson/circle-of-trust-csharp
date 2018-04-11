using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;

namespace AndrewLarsson.CircleOfTrust.Domain.Repositories {
	public interface IBetrayedCircleRepository {
		Task<BetrayedCircle> FindBetrayedCircle(Guid betrayedCircleId);
		Task<BetrayedCircle> FindBetrayedCircleByCircleId(Guid circleId);
	}
}
