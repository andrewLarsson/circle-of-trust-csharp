using System;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Events;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public class BetrayCircleService {
		public static BetrayedCircle BetrayCircle(Guid betrayedCircleId, Circle circle, Guid playerId, string key) {
			ValidateKeyService.ValidateKey(key, circle.Key);
			BetrayedCircle betrayedCircle = new BetrayedCircle(betrayedCircleId, circle.Id, playerId);
			betrayedCircle.RaiseEvent(new CircleBetrayedEvent(betrayedCircle.Id, betrayedCircle.CircleId, betrayedCircle.PlayerId));
			return betrayedCircle;
		}
	}
}
