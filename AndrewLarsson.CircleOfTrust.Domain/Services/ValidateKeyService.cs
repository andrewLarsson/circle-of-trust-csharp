using AndrewLarsson.CircleOfTrust.Domain.Exceptions;

namespace AndrewLarsson.CircleOfTrust.Domain.Services {
	public static class ValidateKeyService {
		public static void ValidateKey(string key1, string key2) {
			if (key1 != key2) {
				throw new KeyInvalidException();
			}
		}
	}
}
