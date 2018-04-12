using System.Data;

namespace AndrewLarsson.CircleOfTrust.View.Dapper {
	public class CircleOfTrustDapperViewContext {
		public IDbConnection DbConnection { get; }

		public CircleOfTrustDapperViewContext(IDbConnection dbConnection) {
			DbConnection = dbConnection;
		}
	}
}
