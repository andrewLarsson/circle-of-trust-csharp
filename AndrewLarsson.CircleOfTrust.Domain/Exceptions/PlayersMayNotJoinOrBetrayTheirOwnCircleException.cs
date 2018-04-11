using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class PlayersMayNotJoinOrBetrayTheirOwnCircleException : Exception {
		public PlayersMayNotJoinOrBetrayTheirOwnCircleException()
			: base("Players may not join or betray their own circle.") {
		}
	}
}
