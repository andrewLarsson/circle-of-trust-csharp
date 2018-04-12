using System;
using AndrewLarsson.Common.View;

namespace AndrewLarsson.Common.Host.Providers {
	public interface IQueryHandlerProvider {
		IQueryHandler<TQuery> GetQueryHandler<TQuery>() where TQuery : IQuery;
		object GetQueryHandler(Type queryType);
	}
}
