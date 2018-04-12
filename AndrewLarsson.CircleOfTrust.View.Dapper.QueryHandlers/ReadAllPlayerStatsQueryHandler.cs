using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.View;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public class ReadAllPlayerStatsQueryHandler : IQueryHandler<ReadAllPlayerStatsQuery> {
		private static readonly string LoadPlayerStatsPaged = @"SELECT * FROM PlayerStats ORDER BY [ClusterId]";
		private static readonly string LoadPlayerStatsCount = @"SELECT COUNT_BIG(*) FROM PlayerStats;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public ReadAllPlayerStatsQueryHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task<object> HandleAsync(ReadAllPlayerStatsQuery query) {
			return await _viewContext.DbConnection.QueryPaginateAsync<PlayerStats>(LoadPlayerStatsPaged, LoadPlayerStatsCount, query.PagingMetaData);
		}
	}
}
