using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.AggregateRoots {
	public class Player : AggregateRoot {
		public string Username { get; }
		public string Password { get; }

		public Player(Guid id, string username, string password) {
			Id = id;
			Username = username;
			Password = password;
		}
	}
}
