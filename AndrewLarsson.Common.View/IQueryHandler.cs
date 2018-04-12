using System.Threading.Tasks;

namespace AndrewLarsson.Common.View {
	public interface IQueryHandler<TQuery> where TQuery: IQuery {
		Task<object> HandleAsync(TQuery query);
	}
}
