using System;

namespace AndrewLarsson.CircleOfTrust.Persistence.Models {
	public class Player {
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
