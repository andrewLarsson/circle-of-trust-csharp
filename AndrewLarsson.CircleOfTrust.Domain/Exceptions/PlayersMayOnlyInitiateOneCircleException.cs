using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class PlayersMayOnlyInitiateOneCircleException : Exception {
		public PlayersMayOnlyInitiateOneCircleException()
			: base("Players may only initiate one circle.") {
		}
	}
}
