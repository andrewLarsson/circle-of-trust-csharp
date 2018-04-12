using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.Events;
using AndrewLarsson.Common.Domain;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.EventHandlers {
	public class PlayerRegisteredPlayerStatsEventHandler : IEventHandler<PlayerRegisteredEvent> {
		private static readonly string InsertPlayerStatsFromPlayerRegisteredEvent = @"INSERT INTO PlayerStats ([PlayerId], [Username], [HasCircle], [MemberOfCircles], [BetrayedCircles]) VALUES (@AggregateRootId, @Username, 0, 0, 0);";
		private readonly CircleOfTrustDapperViewContext _viewContext;

		public PlayerRegisteredPlayerStatsEventHandler(CircleOfTrustDapperViewContext viewContext) {
			_viewContext = viewContext;
		}

		public Task HandleAsync(PlayerRegisteredEvent playerRegisteredEvent) {
			return _viewContext.DbConnection.ExecuteAsync(InsertPlayerStatsFromPlayerRegisteredEvent, playerRegisteredEvent);
		}
	}
}
