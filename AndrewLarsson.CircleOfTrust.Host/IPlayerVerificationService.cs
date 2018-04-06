using System.Threading.Tasks;

namespace AndrewLarsson.CircleOfTrust.Host {
	public interface IPlayerVerificationService {
		Task VerifyPlayer(PlayerVerification playerVerification);
	}
}
