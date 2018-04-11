using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class CircleKeyMustBeValidInOrderToJoinOrBetrayCircleException : Exception {
		public CircleKeyMustBeValidInOrderToJoinOrBetrayCircleException()
			: base("Circle key must be valid in order to join or betray circle.") {
		}
	}
}
