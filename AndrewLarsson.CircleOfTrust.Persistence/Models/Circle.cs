using System;

namespace AndrewLarsson.CircleOfTrust.Persistence.Models {
	public class Circle {
		public Guid Id { get; set; }
		public Guid PlayerId { get; set; }
		public string Name { get; set; }
		public string Key { get; set; }
	}
}
