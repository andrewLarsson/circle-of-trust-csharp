using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.AggregateRoots {
	public class BetrayedCircle : AggregateRoot {
		public Guid CircleId { get; }
		public Guid PlayerId { get; }

		public BetrayedCircle(Guid id, Guid circleId, Guid playerId) {
			Id = id;
			CircleId = circleId;
			PlayerId = playerId;
		}
	}
}
