using System.Data;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper {
	public class CircleOfTrustDapperPersistenceContext {
		public IDbConnection DbConnection { get; }

		public CircleOfTrustDapperPersistenceContext(IDbConnection dbConnection) {
			DbConnection = dbConnection;
		}
	}
}
