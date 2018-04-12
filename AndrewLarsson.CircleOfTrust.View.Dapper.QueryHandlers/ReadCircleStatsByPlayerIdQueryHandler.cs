using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.View;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public class ReadCircleStatsByPlayerIdQueryHandler : IQueryHandler<ReadCircleStatsByPlayerIdQuery> {
		private static readonly string LoadCircleStatsByCircleId = @"SELECT * FROM CircleStats WHERE [PlayerId] = @PlayerId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public ReadCircleStatsByPlayerIdQueryHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task<object> HandleAsync(ReadCircleStatsByPlayerIdQuery query) {
			return await _viewContext.DbConnection.QueryFirstOrDefaultAsync<CircleStats>(LoadCircleStatsByCircleId, query);
		}
	}
}
