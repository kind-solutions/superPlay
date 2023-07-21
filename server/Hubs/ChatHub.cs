using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Google.Protobuf;

using Superplay.Protobuf.Messages;

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
        public async Task Login(byte[] payload)
        {
            var login = LoginRequest.Parser.ParseFrom(payload);

            _logger.LogTrace($"Received login request from udid={login.Udid}"); 

            var playerId = Guid.NewGuid().ToString(); //TODO

            var res = new LoginResponse
            {
                Myself = new Player {
                    Id = playerId,
                },
            };
            await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
        }



        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task UpdateResources(byte[] payload)
        {
            var req = UpdateResourcesRequest.Parser.ParseFrom(payload);

            _logger.LogInformation($"{nameof(UpdateResources)} Received {req} with size {payload.Count()} B");
            await Task.Delay(1);
            //TODO

        }

        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task SendGift(byte[] payload)
        {
            await Task.Delay(1);
            //TODO
        }

    }
}