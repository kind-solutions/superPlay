using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Google.Protobuf;

using Superplay.Authorization;
using Superplay.Data;
using Superplay.Protobuf.Messages;

namespace Superplay.Hubs;
public class ChatHub : Hub
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ChatHub> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly ISessionCacheHandler _cache;

    public ChatHub(ILogger<ChatHub> logger, ApplicationDbContext dbContext, ISessionCacheHandler cache, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _dbContext = dbContext;
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
    }

    [AllowAnonymous]
    public async Task Login(byte[] payload)
    {
        var res = new LoginResponse { };

        if (!_cache.TryGetDeviceFromConnection(Context.ConnectionId, out Guid deviceId))
        {
            _logger.LogInformation($"[{nameof(Login)}] Device already logged in deviceid={deviceId}");
            await Clients.Caller.SendAsync("LoginResponse", res.ToByteArray());
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

    [Authorize(Policy = "CustomHubAuthorizatioPolicy")]
    public async Task UpdateResources(byte[] payload)
    {
        var player = await SuperplayUserProvider.TryGetPlayer(_dbContext, _cache, Context, _logger);

        if (player == null)
        {
            var playerId = SuperplayUserProvider.GetUserId(Context, _cache, _logger);
            _logger.LogWarning($"{nameof(UpdateResources)} The player with GUID={playerId} is not found in the database.");
            return;
        }

        var req = UpdateResourcesRequest.Parser.ParseFrom(payload);

        _logger.LogDebug($"{nameof(UpdateResources)} Received {req} with size {payload.Count()}B");

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
    }

}