using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task Login(string udid)
        {
            _logger.LogInformation("test");

            var playerId = Guid.NewGuid();
            await Clients.Caller.SendAsync("LoginResponse", playerId);
        }



        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task UpdateResources(string user, string message)
        {
            _logger.LogInformation("test");

            await Clients.Others.SendAsync("ReceiveMessage", user, message);
        }

        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task SendGift(string user, string message)
        {
            _logger.LogInformation("test");

            await Clients.Others.SendAsync("ReceiveMessage", user, message);
        }


        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task SendMessage(string user, byte[] message)
        {
            _logger.LogInformation(message[0].ToString());
            _logger.LogInformation(message[1].ToString());
            _logger.LogInformation(message[2].ToString());

        }
    }
}