using System;
using AndrewLarsson.Common.View;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.Common.Host.Providers.DependencyInjection {
	public class QueryHandlerProvider : IQueryHandlerProvider {
		private static readonly Type QueryHandlerOpenType = typeof(IQueryHandler<>);
		private readonly IServiceProvider _serviceProvider;

		public QueryHandlerProvider(IServiceProvider serviceProvider) {
			_serviceProvider = serviceProvider;
		}

		public IQueryHandler<TQuery> GetQueryHandler<TQuery>() where TQuery : IQuery {
			return _serviceProvider.GetService<IQueryHandler<TQuery>>();
		}

		public object GetQueryHandler(Type queryType) {
			Type handlerType = QueryHandlerOpenType.MakeGenericType(queryType);
			return _serviceProvider.GetService(handlerType);
		}
	}
}
