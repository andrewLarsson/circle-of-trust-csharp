using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Models;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.View;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public class ReadPlayerStatsByPlayerIdQueryHandler : IQueryHandler<ReadPlayerStatsByPlayerIdQuery> {
		private static readonly string LoadPlayerStatsByPlayerId = @"SELECT * FROM PlayerStats WHERE [PlayerId] = @PlayerId;";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public ReadPlayerStatsByPlayerIdQueryHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public async Task<object> HandleAsync(ReadPlayerStatsByPlayerIdQuery query) {
			return await _viewContext.DbConnection.QueryFirstOrDefaultAsync<PlayerStats>(LoadPlayerStatsByPlayerId, query);
		}
	}
}
