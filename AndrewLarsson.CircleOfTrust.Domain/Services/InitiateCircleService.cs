using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.CircleOfTrust.Domain.Rules;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class InitiateCircleService {
		public static async Task<Circle> InitiateCircle(
			Guid circleId,
			Guid playerId,
			string name,
			string key,
			CirclesMustHaveAUniqueNameRule circlesMustHaveAUniqueNameRule,
			PlayersMayOnlyInitiateOneCircleRule playersMayOnlyInitiateOneCircleRule
		) {
			await circlesMustHaveAUniqueNameRule.Verify(name);
			await playersMayOnlyInitiateOneCircleRule.Verify(playerId);
			Circle circle = new Circle(circleId, playerId, name, key);
			circle.RaiseEvent(new CircleInitiatedEvent(circle.Id, circle.PlayerId, circle.Name, circle.Key));
			return circle;
		}
	}
}
