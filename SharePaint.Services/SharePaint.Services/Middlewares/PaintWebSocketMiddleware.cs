using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharePaint.Services.ServiceProviders;
using ShareStroke.Services.ServiceProviders;

namespace SharePaint.Services.Middlewares
{
    public class PaintWebSocketMiddleware
    {
        public PaintWebSocketMiddleware()
        {
        }

        private static ConcurrentDictionary<Guid, WebSocket> _sockets = new ConcurrentDictionary<Guid, WebSocket>();

        private readonly RequestDelegate _next;

        public PaintWebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            IUserServies userService = context.RequestServices.GetService(typeof(IUserServies)) as IUserServies;
            IReceiveDataHandler receiveDataHandler = context.RequestServices.GetService(typeof(IReceiveDataHandler)) as IReceiveDataHandler;

            CancellationToken ct = context.RequestAborted;
            var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
            var socketId = Guid.NewGuid().ToString();

            //for new socket connection create new User
            var user = userService.CreateUser();
            var resp = JsonConvert.SerializeObject( new { user });
            await SendStringAsync(currentSocket, resp, ct);
            _sockets.TryAdd(user.Id, currentSocket);

            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                var response = await ReceiveStringAsync(currentSocket, ct);
                if (string.IsNullOrEmpty(response))
                {
                    if (currentSocket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    continue;
                }

                 var result =   receiveDataHandler.Handle(response);
                var currentUser = userService.Get(user.Id);
                var usersOfPaint = userService.GetUsersByPaintId(currentUser.PaintId);
                foreach (var socket in _sockets)
                {
                    if (socket.Value.State == WebSocketState.Open && socket.Key != user.Id && usersOfPaint.Any(e => e.Id == socket.Key))
                    {
                        await SendStringAsync(socket.Value, response, ct);
                    }
                }
                
            }

            WebSocket dummy;
            _sockets.TryRemove(user.Id, out dummy);

            await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
            currentSocket.Dispose();
        }

        private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }

        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }

                // Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
