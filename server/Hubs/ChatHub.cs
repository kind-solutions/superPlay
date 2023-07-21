using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IWebSocketsManager _connectionManager;

        public ChatHub(ILogger<ChatHub> logger, IWebSocketsManager manager)
        {
            _logger = logger;
            _connectionManager = manager;
        }

        public async Task SendMessage(string user, string message)
        {
            _logger.LogInformation("test");
            await Clients.Others.SendAsync("ReceiveMessage", user, message);
        }
    }
}