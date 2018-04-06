using System.Threading.Tasks;

namespace AndrewLarsson.CircleOfTrust.Host {
	public class PlayerVerificationService : IPlayerVerificationService {
		public PlayerVerificationService() {
		}

		public Task VerifyPlayer(PlayerVerification playerVerification) {
			// TODO Perform captcha verification.
			return Task.FromResult<object>(null);
		}
	}
}
