using System;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class InitiateCircleCommand : ICommand {
		public Guid CircleId { get; set; }
		public Guid PlayerId { get; set; }
		public string Name { get; set; }
		public string Key { get; set; }
	}
}
