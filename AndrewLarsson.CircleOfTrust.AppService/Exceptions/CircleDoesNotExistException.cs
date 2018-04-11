using System;

namespace AndrewLarsson.CircleOfTrust.AppService.Exceptions {
	public class CircleDoesNotExistException : Exception {
		public CircleDoesNotExistException()
			: base("Circle does not exist.") {
		}
	}
}
