using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Google.Protobuf.Examples.AddressBook;
using Google.Protobuf;

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

            var playerId = Guid.NewGuid(); //TODO
            await Clients.Caller.SendAsync("LoginResponse", playerId);
        }



        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task UpdateResources(byte[] payload)
        {
            await Task.Delay(1);
            //TODO

        }

        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task SendGift(byte[] payload)
        {
            await Task.Delay(1);
            //TODO
        }


        [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
        public async Task SendMessage(string user, byte[] message)
        {
            await Task.Delay(1);
            var p = Person.Parser.ParseFrom(message);
            _logger.LogInformation(p.Email);

        }
    }
}