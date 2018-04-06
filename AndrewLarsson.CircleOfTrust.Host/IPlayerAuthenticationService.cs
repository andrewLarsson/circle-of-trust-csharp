using System;
using System.Threading.Tasks;

namespace AndrewLarsson.CircleOfTrust.Host {
	public interface IPlayerAuthenticationService {
		Task<Guid> AuthenticatePlayer(PlayerAuthentication playerAuthentication);
		Task AuthenticateAndAuthorizePlayer(PlayerAuthentication playerAuthentication, Guid playerId);
	}
}
