using System;

namespace AndrewLarsson.CircleOfTrust.AppService.Exceptions {
	public class PlayerDoesNotExistException : Exception {
		public PlayerDoesNotExistException()
			: base("Player does not exist.") {
		}
	}
}
