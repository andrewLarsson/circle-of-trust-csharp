using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.View.Queries;
using AndrewLarsson.Common.Host.Providers;
using AndrewLarsson.Common.View;
using EdjCase.JsonRpc.Router;

namespace AndrewLarsson.CircleOfTrust.Host.HttpJsonRpc.Controllers {
	[RpcRoute("api/rpc.json")]
	public class ReadController : RpcController {
		private readonly IQueryHandlerProvider _queryHandlerProvider;

		public ReadController(IQueryHandlerProvider queryHandlerProvider) {
			_queryHandlerProvider = queryHandlerProvider;
		}

		public Task ReadPlayerStatsByPlayerId(ReadPlayerStatsByPlayerIdQuery query) {
			IQueryHandler<ReadPlayerStatsByPlayerIdQuery> queryHandler = _queryHandlerProvider.GetQueryHandler<ReadPlayerStatsByPlayerIdQuery>();
			return queryHandler.HandleAsync(query);
		}

		public Task ReadCircleStatsByCircleId(ReadCircleStatsByCircleIdQuery query) {
			IQueryHandler<ReadCircleStatsByCircleIdQuery> queryHandler = _queryHandlerProvider.GetQueryHandler<ReadCircleStatsByCircleIdQuery>();
			return queryHandler.HandleAsync(query);
		}

		public Task ReadCircleStatsByPlayerId(ReadCircleStatsByPlayerIdQuery query) {
			IQueryHandler<ReadCircleStatsByPlayerIdQuery> queryHandler = _queryHandlerProvider.GetQueryHandler<ReadCircleStatsByPlayerIdQuery>();
			return queryHandler.HandleAsync(query);
		}

		public Task ReadAllPlayerStats(ReadAllPlayerStatsQuery query) {
			IQueryHandler<ReadAllPlayerStatsQuery> queryHandler = _queryHandlerProvider.GetQueryHandler<ReadAllPlayerStatsQuery>();
			return queryHandler.HandleAsync(query);
		}

		public Task ReadAllCircleStats(ReadAllCircleStatsQuery query) {
			IQueryHandler<ReadAllCircleStatsQuery> queryHandler = _queryHandlerProvider.GetQueryHandler<ReadAllCircleStatsQuery>();
			return queryHandler.HandleAsync(query);
		}

		public Task ReadCircleLeaderboard(ReadCircleLeaderboardQuery query) {
			IQueryHandler<ReadCircleLeaderboardQuery> queryHandler = _queryHandlerProvider.GetQueryHandler<ReadCircleLeaderboardQuery>();
			return queryHandler.HandleAsync(query);
		}
	}
}
