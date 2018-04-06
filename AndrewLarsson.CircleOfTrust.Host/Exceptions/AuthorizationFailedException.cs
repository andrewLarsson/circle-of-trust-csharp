using System;

namespace AndrewLarsson.CircleOfTrust.Host.Exceptions {
	public class AuthorizationFailedException : Exception {
		public AuthorizationFailedException(string message)
			: base(message) {
		}
	}
}
