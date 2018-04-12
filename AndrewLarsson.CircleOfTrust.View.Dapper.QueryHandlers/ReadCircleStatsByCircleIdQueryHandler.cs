using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.View;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public class ReadCircleStatsByCircleIdQueryHandler : IQueryHandler<ReadCircleStatsByCircleIdQuery> {
		private static readonly string LoadCircleStatsByCircleId = @"SELECT * FROM CircleStats WHERE [CircleId] = @CircleId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public ReadCircleStatsByCircleIdQueryHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task<object> HandleAsync(ReadCircleStatsByCircleIdQuery query) {
			return await _viewContext.DbConnection.QueryFirstOrDefaultAsync<CircleStats>(LoadCircleStatsByCircleId, query);
		}
	}
}
