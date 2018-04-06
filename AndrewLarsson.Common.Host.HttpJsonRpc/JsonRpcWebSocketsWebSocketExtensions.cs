using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EdjCase.JsonRpc.Router;
using EdjCase.JsonRpc.Router.Abstractions;
using EdjCase.JsonRpc.Router.Defaults;
using Microsoft.AspNetCore.Http;

namespace AndrewLarsson.Common.Host.HttpJsonRpc {
	public static class JsonRpcWebSocketsWebSocketExtensions {
		public static async Task HandleJsonRpcWebSocketRequest(this WebSocket webSocket, HttpContext context, IRpcRequestHandler rpcRequestHandler, IRpcRouteProvider rpcRouteProvider) {
			byte[] buffer = new byte[1024 * 4];
			IRouteContext routeContext = DefaultRouteContext.FromHttpContext(context, rpcRouteProvider);
			while (webSocket.State == WebSocketState.Open) {
				WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				if (result.MessageType != WebSocketMessageType.Text) {
					continue;
				}
				string requestBody = Encoding.ASCII.GetString(buffer);
				string response = await rpcRequestHandler.HandleRequestAsync(RpcPath.Parse(context.Request.Path), requestBody, routeContext);
				await webSocket.SendAsync(
					new ArraySegment<byte>(Encoding.ASCII.GetBytes(response), 0, response.Length),
					WebSocketMessageType.Text,
					true,
					CancellationToken.None
				);
			}
		}
	}
}
