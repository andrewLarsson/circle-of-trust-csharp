﻿using System;
using System.Data;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.Persistence.Dapper.Repositories {
	public class DapperCircleRepository : ICircleRepository {
		private static readonly string LoadCircle = @"SELECT * FROM Circle WHERE Id = @Id;";
		private static readonly string LoadCircleByName = @"SELECT * FROM Circle WHERE Name = @Name;";
		private static readonly string LoadCircleByPlayerId = @"SELECT * FROM Circle WHERE PlayerId = @PlayerId;";
		private readonly IDbConnection _dbConnection;

		public DapperCircleRepository(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<Circle> FindCircle(Guid circleId) {
			Models.Circle circle = await _dbConnection.QueryFirstOrDefaultAsync<Models.Circle>(LoadCircle, new { Id = circleId });
			if (circle == null) {
				return null;
			}
			return new Circle(circle.Id, circle.PlayerId, circle.Name, circle.Key);
		}

		public async Task<Circle> FindCircleByName(string name) {
			Models.Circle circle = await _dbConnection.QueryFirstOrDefaultAsync<Models.Circle>(LoadCircleByName, new { Name = name });
			if (circle == null) {
				return null;
			}
			return new Circle(circle.Id, circle.PlayerId, circle.Name, circle.Key);
		}

		public async Task<Circle> FindCircleByPlayerId(Guid playerId) {
			Models.Circle circle = await _dbConnection.QueryFirstOrDefaultAsync<Models.Circle>(LoadCircleByPlayerId, new { PlayerId = playerId });
			if (circle == null) {
				return null;
			}
			return new Circle(circle.Id, circle.PlayerId, circle.Name, circle.Key);
		}
	}
}
