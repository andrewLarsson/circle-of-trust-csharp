using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class PlayersMayOnlyJoinACircleOnceException : Exception {
		public PlayersMayOnlyJoinACircleOnceException()
			: base("Players may only join a circle once.") {
		}
	}
}
