using System;

namespace AndrewLarsson.CircleOfTrust.View.Models {
	public class PlayerStats {
		public Guid PlayerId { get; set; }
		public string Username { get; set; }
		public bool HasCircle { get; set; }
		public int MemberOfCircles { get; set; }
		public int BetrayedCircles { get; set; }
	}
}
