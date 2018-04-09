using System;

namespace AndrewLarsson.CircleOfTrust.View.Models {
	public class CircleStats {
		public Guid CircleId { get; set; }
		public Guid PlayerId { get; set; }
		public string Name { get; set; }
		public bool IsBetrayed { get; set; }
		public int Members { get; set; }
	}
}
