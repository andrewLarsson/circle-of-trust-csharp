using System;
using AndrewLarsson.Common.Domain;

namespace AndrewLarsson.CircleOfTrust.Domain.AggregateRoots {
	public class Circle : AggregateRoot {
		public Guid PlayerId { get; }
		public string Name { get; }
		public string Key { get; }

		public Circle(Guid id, Guid playerId, string name, string key) {
			Id = id;
			PlayerId = playerId;
			Name = name;
			Key = key;
		}
	}
}
