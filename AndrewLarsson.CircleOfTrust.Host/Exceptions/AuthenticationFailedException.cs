using System;

namespace AndrewLarsson.CircleOfTrust.Host.Exceptions {
	public class AuthenticationFailedException : Exception {
		public AuthenticationFailedException(string message)
			: base(message) {
		}
	}
}
