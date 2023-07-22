using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Google.Protobuf;

using Superplay.Authorization;
using Superplay.Data;
using Superplay.Protobuf.Messages;

namespace Superplay.Hubs;
public class EconomyHub : Hub
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<EconomyHub> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly ISessionCacheHandler _cache;

    public EconomyHub(ILogger<EconomyHub> logger, ApplicationDbContext dbContext, ISessionCacheHandler cache, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _dbContext = dbContext;
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
    }
    public override Task OnConnectedAsync()
    {
        var deviceId = SuperplayUserProvider.GetDeviceId(_httpContextAccessor);
        if (deviceId != Guid.Empty)
        {
            _cache.CacheDeviceConnection(deviceId, Context.ConnectionId);
        }

        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _cache.DeleteSession(Context.ConnectionId);
        return base.OnConnectedAsync();
    }
    [AllowAnonymous]
    public async Task Login()
    {
        var res = new LoginResponse { };

        //token is sent via Auth Header or query param
        var deviceId = SuperplayUserProvider.GetDeviceId(_httpContextAccessor);

        if (deviceId == Guid.Empty)
        {
            _logger.LogWarning($"[{nameof(Login)}] Invalid request");
            await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
            return;
        }

        _logger.LogTrace($"[{nameof(Login)}] Request from con={Context.ConnectionId} and deviceId={deviceId.ToString()}");
        if (_cache.TryGetDeviceFromConnection(Context.ConnectionId, out Guid id))
        {
            if (deviceId != id)
            {
                _logger.LogInformation($"[{nameof(Login)}] Device already logged in deviceid={deviceId}");
                await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
                return;
            }
        }

        var device = await _dbContext.Devices.FindAsync(deviceId);

        if (device == null)
        {
            _logger.LogInformation($"[{nameof(Login)}] did not find device id={deviceId}");

            await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
            return;

        }

        if (_cache.IsPlayerCached(device.PlayerId))
        {
            _logger.LogInformation($"[{nameof(Login)}] Player {device.PlayerId} tried to log in from deviceId={device.Id} but is already logged in on antoher device");
            await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
            return;
        }

        _cache.CacheSession(Context.ConnectionId, device.Id, device.PlayerId);

        res.Myself = new Player
        {
            Id = device.PlayerId.ToString(),
        };

        await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
    }



    [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
    public async Task UpdateResources(byte[] payload)
    {
        var player = await SuperplayUserProvider.TryGetCaller(_dbContext, _cache, Context, _logger);

        if (player == null)
        {
            var playerId = SuperplayUserProvider.GetUserId(Context, _cache, _logger);
            _logger.LogWarning($"{nameof(UpdateResources)} The player with GUID={playerId} is not found in the database.");
            return;
        }

        var req = UpdateResourcesRequest.Parser.ParseFrom(payload);
        if (req == null)
        {
            _logger.LogWarning($"{nameof(UpdateResources)} Bad request.");
            return;
        }

        _logger.LogDebug($"{nameof(UpdateResources)} Received {req} with size {payload.Count()}B");

        _logger.LogTrace($"{nameof(UpdateResources)} player {player.Id} has {player.Coins} coins and {player.Rolls} rolls before update");

        switch (req.Type)
        {
            case ResourceType.Coins:

                //we try to remove coins but player does not have enough
                if (req.Ammount.Value < 0 && player.Coins < Math.Abs(req.Ammount.Value))
                {

                }
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
        var player = await SuperplayUserProvider.TryGetCaller(_dbContext, _cache, Context, _logger);

        if (player == null)
        {
            var playerId = SuperplayUserProvider.GetUserId(Context, _cache, _logger);
            _logger.LogWarning($"{nameof(UpdateResources)} The player with GUID={playerId} is not found in the database.");
            return;
        }

        var req = SendGiftRequest.Parser.ParseFrom(payload);

        //validate request {valid friend and ammount}
        if (req == null || !Guid.TryParse(req.FriendId, out var friendId) || req.Ammount.Value <= 0)
        {
            _logger.LogWarning($"{nameof(SendGift)} Bad request.");
            return;
        }

        _logger.LogDebug($"[{nameof(SendGift)}] Received {req} with size {payload.Count()}B");

        var friend = await SuperplayUserProvider.TryGetPlayer(friendId, _dbContext, _cache, Context, _logger);

        if (friend == null)
        {
            _logger.LogWarning($"{nameof(SendGift)} The Friend player with GUID={friendId} is not found in the database.");
            return;
        }

        if (friend.Id == player.Id)
        {
            _logger.LogWarning($"{nameof(SendGift)} The Player player with GUID={friendId} tried to gift himself.");
            return;
        }
        switch (req.Type)
        {
            case ResourceType.Coins:
                if (player.Coins < req.Ammount.Value)
                {
                    _logger.LogWarning($"{nameof(SendGift)} The Player {player.Id} does not have enough Coins.");
                    return;
                }
                friend.Coins += req.Ammount.Value;
                player.Coins -= req.Ammount.Value;
                break;
            case ResourceType.Rolls:
                if (player.Rolls < req.Ammount.Value)
                {
                    _logger.LogWarning($"{nameof(SendGift)} The Player {player.Id} does not have enough Coins.");
                    return;
                }
                friend.Rolls += req.Ammount.Value;
                player.Rolls -= req.Ammount.Value;
                break;
            default:
                _logger.LogWarning($"{nameof(UpdateResources)} Unknown resourceType {req.Type}");
                break;
        }

        //friend is online
        if (_cache.TryGetPlayerConnection(friend.Id, out var friendConnection) && friendConnection != null)
        {
            var res = new GiftEvent
            {
                From = new Player { Id = player.Id.ToString() },
                Ammount = req.Ammount,
                Type = req.Type,
            };

            await Clients.Client(friendConnection).SendAsync("GiftEvent", res.ToByteArray());
        }

    }

}