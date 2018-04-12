using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.View;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public class ReadCircleLeaderboardQueryHandler : IQueryHandler<ReadCircleLeaderboardQuery> {
		private static readonly string LoadCircleLeaderboardContenderPaged = @"SELECT * FROM CircleLeaderboardContender ORDER BY [Members] DESC";
		private static readonly string LoadCircleLeaderboardContenderCount = @"SELECT COUNT_BIG(*) FROM CircleLeaderboardContender;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public ReadCircleLeaderboardQueryHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task<object> HandleAsync(ReadCircleLeaderboardQuery query) {
			return await _viewContext.DbConnection.QueryPaginateAsync<CircleLeaderboardContender>(LoadCircleLeaderboardContenderPaged, LoadCircleLeaderboardContenderCount, query.PagingMetaData);
		}
	}
}
