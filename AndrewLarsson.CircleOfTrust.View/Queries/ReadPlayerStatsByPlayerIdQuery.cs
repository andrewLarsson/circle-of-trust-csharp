﻿using System;
using AndrewLarsson.Common.View;

namespace AndrewLarsson.CircleOfTrust.View.Queries {
	public class ReadPlayerStatsByPlayerIdQuery : IQuery {
		public Guid PlayerId { get; set; }
	}
}
