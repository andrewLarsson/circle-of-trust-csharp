using System;
using AndrewLarsson.Common.AppService;

namespace AndrewLarsson.CircleOfTrust.AppService.Commands {
	public class RegisterPlayerCommand : ICommand {
		public Guid PlayerId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
