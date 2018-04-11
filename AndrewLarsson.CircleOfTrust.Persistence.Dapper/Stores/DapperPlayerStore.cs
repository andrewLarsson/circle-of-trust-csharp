﻿using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.Common.AppService;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Stores {
	public class DapperPlayerStore : IAggregateRootStore<Player> {
		private static readonly string LoadPlayer = @"SELECT * FROM Player WHERE Id = @Id;";
		private readonly IDbConnection _dbConnection;

		public DapperPlayerStore(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<Player> LoadAsync(Guid id) {
			Models.Player player = await _dbConnection.QueryFirstOrDefaultAsync<Models.Player>(LoadPlayer, new { Id = id });
			if (player == null) {
				return null;
			}
			return new Player(player.Id, player.Username, player.Password);
		}

		public Task SaveAsync(Player player) {
			return Task.FromResult<object>(null);
		}
	}
}