using System;
using AndrewLarsson.Common.View;

namespace AndrewLarsson.CircleOfTrust.View.Queries {
	public class ReadCircleStatsByCircleIdQuery : IQuery {
		public Guid CircleId { get; set; }
	}
}
