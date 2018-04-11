using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class PlayersMustHaveAUniqueUserameException : Exception {
		public PlayersMustHaveAUniqueUserameException()
			: base("Players must have a unique userame.") {
		}
	}
}
