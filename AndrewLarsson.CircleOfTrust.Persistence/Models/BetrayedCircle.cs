using System;

namespace AndrewLarsson.CircleOfTrust.Persistence.Models {
	public class BetrayedCircle {
		public Guid Id { get; set; }
		public Guid CircleId { get; set; }
		public Guid PlayerId { get; set; }
	}
}
