using System;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class InitiateCircleService {
		public static Circle InitiateCircle(Guid circleId, Guid playerId, string name, string key) {
			Circle circle = new Circle(circleId, playerId, name, key);
			circle.RaiseEvent(new CircleInitiatedEvent(circle.Id, circle.PlayerId, circle.Name, circle.Key));
			return circle;
		}
	}
}
