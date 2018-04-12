using System;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class JoinCircleCommand : ICommand {
		public Guid MemberId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid CircleId { get; set; }
		public string Key { get; set; }
	}
}
