using System;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class BetrayCircleCommand : ICommand {
		public Guid BetrayedCircleId { get; set; }
		public Guid CircleId { get; set; }
		public Guid PlayerId { get; set; }
		public string Key { get; set; }
	}
}
