using System;
using System.Net.WebSockets;
using EdjCase.JsonRpc.Router.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AndrewLarsson.Common.Host.HttpJsonRpc {
	public static class JsonRpcWebSocketsApplicationBuilderExtensions {
		public static IApplicationBuilder UseJsonRpcWithWebSocketsSupport(this IApplicationBuilder application) {
			return application
				.UseWebSockets()
				.UseJsonRpcWebSockets()
				.UseJsonRpc(application.ApplicationServices.GetService<IRpcRouteProvider>())
			;
		}

		private static IApplicationBuilder UseJsonRpcWebSockets(this IApplicationBuilder application) {
			IServiceProvider serviceProvider = application.ApplicationServices;
			return application.Use(async (context, next) => {
				if (context.WebSockets.IsWebSocketRequest) {
					WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
					await webSocket.HandleJsonRpcWebSocketRequest(context, serviceProvider.GetService<IRpcRequestHandler>(), serviceProvider.GetService<IRpcRouteProvider>());
				} else {
					await next();
				}
			});
		}
	}
}
