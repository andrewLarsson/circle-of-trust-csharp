using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class CirclesMustHaveAUniqueNameException : Exception {
		public CirclesMustHaveAUniqueNameException()
			: base("Circles must have a unique name.") {
		}
	}
}
