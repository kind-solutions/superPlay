using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Google.Protobuf;

using Superplay.Authorization;
using Superplay.Data;
using Superplay.Protobuf.Messages;

namespace Superplay.Hubs;
    public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly ApplicationDbContext _dbContext;
    readonly IHttpContextAccessor _httpContextAccessor;

    public ChatHub(ILogger<ChatHub> logger, ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
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
        var player = await SuperplayUserProvider.TryGetPlayer(_dbContext, _httpContextAccessor, _logger);

        if (player == null)
        {
            var playerId = SuperplayUserProvider.GetUserId(_httpContextAccessor, _logger);
            _logger.LogWarning($"{nameof(UpdateResources)} The player with GUID={playerId} is not found in the database.");
            return;
        }

        var req = UpdateResourcesRequest.Parser.ParseFrom(payload);

        _logger.LogTrace($"{nameof(UpdateResources)} Received {req} with size {payload.Count()}B");
        _logger.LogTrace($"{nameof(UpdateResources)} player {player.Id} has {player.Coins} coins and {player.Rolls} rolls before update");

        switch (req.Type)
        {
            case ResourceType.Coins:
                player.Coins += req.Ammount.Value;
                break;
            case ResourceType.Rolls:
                player.Rolls += req.Ammount.Value;
                break;
            default:
                _logger.LogWarning($"{nameof(UpdateResources)} Unknown resourceType {req.Type}");
                break;
        }
        _logger.LogTrace($"{nameof(UpdateResources)} player {player.Id} has {player.Coins} coins and {player.Rolls} rolls after update");

        await _dbContext.SaveChangesAsync();

    }

    [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
    public async Task SendGift(byte[] payload)
    {
        await Task.Delay(1);
        //TODO
    }

}