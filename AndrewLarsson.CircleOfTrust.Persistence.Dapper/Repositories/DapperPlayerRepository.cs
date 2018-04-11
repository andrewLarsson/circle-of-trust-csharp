using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Repositories {
	public class DapperPlayerRepository : IPlayerRepository {
		private static readonly string LoadPlayer = @"SELECT * FROM Player WHERE Id = @Id;";
		private static readonly string LoadPlayerByUsername = @"SELECT * FROM Player WHERE Username = @Username;";
		private readonly IDbConnection _dbConnection;

		public DapperPlayerRepository(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<Player> FindPlayer(Guid playerId) {
			Models.Player player = await _dbConnection.QueryFirstOrDefaultAsync<Models.Player>(LoadPlayer, new { Id = playerId });
			if (player == null) {
				return null;
			}
			return new Player(player.Id, player.Username, player.Password);
		}

		public async Task<Player> FindPlayerByUsername(string username) {
			Models.Player player = await _dbConnection.QueryFirstOrDefaultAsync<Models.Player>(LoadPlayerByUsername, new { Username = username });
			if (player == null) {
				return null;
			}
			return new Player(player.Id, player.Username, player.Password);
		}
	}
}
