using System;

namespace AndrewLarsson.CircleOfTrust.Persistence.Models {
	public class Member {
		public Guid Id { get; set; }
		public Guid PlayerId { get; set; }
		public Guid CircleId { get; set; }
	}
}
