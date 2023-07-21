using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Google.Protobuf;

using Superplay.Data;
using Superplay.Protobuf.Messages;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ChatHub(ILogger<ChatHub> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        public async Task Login(byte[] payload)
        {
            var res = new LoginResponse { };
            var login = LoginRequest.Parser.ParseFrom(payload);

            if (login == null)
            {
                await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
                return;
            }

            _logger.LogTrace($"Received login request from udid={login.Udid}");

            if (!Guid.TryParse(login.Udid, out var deviceId))
            {
                await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
                return;
            }
            var device = await _dbContext.Devices.FindAsync(deviceId);

            if (device == null)
            {
                await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
                return;

            }

            res.Myself = new Player
            {
                Id = device.PlayerId.ToString(),
            };

            await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
        }



        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task UpdateResources(byte[] payload)
        {
            var req = UpdateResourcesRequest.Parser.ParseFrom(payload);

            _logger.LogInformation($"{nameof(UpdateResources)} Received {req} with size {payload.Count()}B");
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