// using System.Net.WebSockets;
// using Microsoft.AspNetCore.Mvc;
// using System.Text;
// namespace WebSocketsSample.Controllers;

// // <snippet>
// public class WebSocketController : ControllerBase
// {
//     private readonly ILogger<WebSocketController> _logger;
//     private readonly IWebSocketsManager _connectionManager;

//     public WebSocketController(ILogger<WebSocketController> logger, IWebSocketsManager manager)
//     {
//         _logger = logger;
//         _connectionManager = manager;
//     }

//     [HttpGet("/ws")]
//     public async Task Get()
//     {
//         if (!HttpContext.WebSockets.IsWebSocketRequest)
//         {
//             _logger.LogWarning("/ws endpoint called without a websocket request");
//             HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
//             return;
//         }

//         using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

//         var sockId = _connectionManager.AddSocket(webSocket);
//         _logger.LogInformation($"Initialized new socket connection with id={sockId}");
//         await HandleConnection(sockId);

//     }
//     // </snippet>

//     private readonly static int MAX_MESSAGE_SIZE = 4 * 1024 * 1024;
//     private async Task HandleConnection(string webSocketId)
//     {
//         if (webSocketId == String.Empty)
//         {
//             _logger.LogWarning($"HandleConnection received an empty socket id");
//             return;
//         }

//         var webSocket = _connectionManager.GetSocketById(webSocketId);

//         if (webSocketId == null)
//         {
//             _logger.LogWarning($"HandleConnection invalid socket for id=${webSocketId}");
//             return;
//         };

//         var buffer = new byte[MAX_MESSAGE_SIZE];
//         var channel = new ArraySegment<byte>(buffer);

//         WebSocketReceiveResult? message;
//         bool dropMessage = false;
//         while (webSocket.State == WebSocketState.Open)
//         {
//             try
//             {
//                 message = await webSocket.ReceiveAsync(channel, CancellationToken.None);
//             }
//             catch (WebSocketException e)
//             {
//                 _logger.LogWarning($"WebSocketController something went wrong while receiveing a message from peer. ErrorCode={e.ErrorCode}, NativeErrCode={e.NativeErrorCode}, Msg={e.Message}");
//                 return;
//             }

//             if (message.MessageType == WebSocketMessageType.Close) break;


//             //we are receiving a multipart message that exceeds size limit
//             if (! message.EndOfMessage)
//             {
//                 if (!dropMessage) //log only once per message
//                 {
//                     _logger.LogWarning($"WebSocketController peer is sending a message bigger than the maximum allowed size of {MAX_MESSAGE_SIZE}. The message will be ignored.");
//                 }

//                 //mark this message to be ignored
//                 dropMessage = true;
//             }
//             else
//             {
                
//                 if (dropMessage)
//                 {
//                     _logger.LogWarning($"WebSocketController The message bigger than the maximum allowed size has been fully received. Dropping the message");
//                     dropMessage = false;
//                 }
//                 else
//                 {
//                     _logger.LogInformation(Encoding.UTF8.GetString(buffer, 0, message.Count));
//                 }
                
//             }
//         }
//     }
// }