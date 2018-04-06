using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.Events {
	public class PlayerRegisteredEvent : DomainEvent {
		public string Username { get; }
		public string Password { get; }

		public PlayerRegisteredEvent(Guid aggregateRootId, string username, string password)
			: base(aggregateRootId) {
			Username = username;
			Password = password;
		}
	}
}
