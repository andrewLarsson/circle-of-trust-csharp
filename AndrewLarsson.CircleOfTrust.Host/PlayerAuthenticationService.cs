using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Host.Exceptions;

namespace AndrewLarsson.CircleOfTrust.Host {
	public class PlayerAuthenticationService : IPlayerAuthenticationService {
		public PlayerAuthenticationService() {
		}

		public Task<Guid> AuthenticatePlayer(PlayerAuthentication playerAuthentication) {
			//TODO Auth
			if (playerAuthentication == null) { //TODO also throw if username/password is incorrect.
				throw new AuthenticationFailedException("Username and/or Password are missing.");
			}
			return (Guid.TryParse(playerAuthentication.Username, out Guid playerId)
				? Task.FromResult(playerId)
				: Task.FromResult(Guid.NewGuid())
			);
		}

		public async Task AuthenticateAndAuthorizePlayer(PlayerAuthentication playerAuthentication, Guid playerId) {
			Guid authenticatedPlayerId = await AuthenticatePlayer(playerAuthentication);
			if (authenticatedPlayerId != playerId) {
				throw new AuthorizationFailedException("Player ID does not match Username.");
			}
		}
	}
}
