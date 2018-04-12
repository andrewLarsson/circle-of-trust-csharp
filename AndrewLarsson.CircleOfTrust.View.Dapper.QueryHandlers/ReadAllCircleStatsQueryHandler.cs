using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.View;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public class ReadAllCircleStatsQueryHandler : IQueryHandler<ReadAllCircleStatsQuery> {
		private static readonly string LoadCircleStatsPaged = @"SELECT * FROM CircleStats ORDER BY [ClusterId]";
		private static readonly string LoadCircleStatsCount = @"SELECT COUNT_BIG(*) FROM CircleStats;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public ReadAllCircleStatsQueryHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task<object> HandleAsync(ReadAllCircleStatsQuery query) {
			return await _viewContext.DbConnection.QueryPaginateAsync<CircleStats>(LoadCircleStatsPaged, LoadCircleStatsCount, query.PagingMetaData);
		}
	}
}
