using System;

namespace AndrewLarsson.CircleOfTrust.Domain.Exceptions {
	public class PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedException : Exception {
		public PlayersMayNotJoinOrBetrayCircleThatHasBeenBetrayedException()
			: base("Players may not join or betray circle that has been betrayed.") {
		}
	}
}
