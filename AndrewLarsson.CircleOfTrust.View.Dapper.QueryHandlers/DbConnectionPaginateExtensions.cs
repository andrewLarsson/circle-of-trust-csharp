using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AndrewLarsson.Common.View;
using Dapper;

namespace AndrewLarsson.CircleOfTrust.View.Dapper.QueryHandlers {
	public static class DbConnectionPaginateExtensions {
		public static async Task<PagingRecordSet<T>> QueryPaginateAsync<T>(this IDbConnection dbConnection, string query, string countQuery, PagingMetaData pagingMetaData, object param = null) where T : class {
			string multiQuery = countQuery + "\n" + query + " OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY;";
			DynamicParameters multiParam = new DynamicParameters(param);
			long offset = (((pagingMetaData.PageNumber < 1)
				? 0
				: pagingMetaData.PageNumber - 1
			) * pagingMetaData.PageSize);
			multiParam.Add("Offset", offset);
			multiParam.Add("Fetch", pagingMetaData.PageSize);
			SqlMapper.GridReader multiQueryResultReader = await dbConnection.QueryMultipleAsync(multiQuery, multiParam);
			return new PagingRecordSet<T>(
				pagingMetaData,
				multiQueryResultReader.Read<long>().FirstOrDefault(),
				multiQueryResultReader.Read<T>()
			);
		}
	}
}
