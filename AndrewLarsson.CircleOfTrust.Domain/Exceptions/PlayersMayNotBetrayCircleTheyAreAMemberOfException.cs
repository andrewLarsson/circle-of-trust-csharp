using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class PlayersMayNotBetrayCircleTheyAreAMemberOfException : Exception {
		public PlayersMayNotBetrayCircleTheyAreAMemberOfException()
			: base("Players may not betray circle they are a member of.") {
		}
	}
}
