//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using Superplay.Data;
using Superplay.Models;

namespace Superplay.Authorization;

public class SuperplayUserProvider : IUserIdProvider
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly ISessionCacheHandler _cache;

    public SuperplayUserProvider(IHttpContextAccessor httpContextAccessor, ISessionCacheHandler cache)
    {
        _httpContextAccessor = httpContextAccessor;
        _cache = cache;
    }

    public string GetUserId(HubConnectionContext connection)
    {
        if (_cache.TryGetSession(connection.ConnectionId, out var session))
        {
            var (_, player) = session;

            return player.ToString();
        }
        return GetDeviceId(_httpContextAccessor).ToString();
    }

    public static Guid GetDeviceId(IHttpContextAccessor _httpContextAccessor)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return Guid.Empty;
        }

        if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var username))
        {
            if (!_httpContextAccessor.HttpContext.Request.Query.TryGetValue("access_token", out username))
            {
                return Guid.Empty;
            }
        }
        var deviceId = username.ToString().Split(" ");
        if (deviceId.Length != 2) // Bearer <token>
        {
            return Guid.Empty;
        }

        if (Guid.TryParse(deviceId[1], out var id))
        {
            return id;
        }
        return Guid.Empty;
    }

    public static Guid GetUserId(Microsoft.AspNetCore.SignalR.HubCallerContext context, ISessionCacheHandler _cache, ILogger _logger)
    {
        if (!_cache.TryGetPlayerFromConnection(context.ConnectionId, out var id))
        {
            _logger.LogWarning($"{nameof(GetUserId)} cannout find playerId for the connection {context.ConnectionId}");
            return Guid.Empty;
        };

        return id;
    }

    public static async Task<Player?> TryGetCaller(ApplicationDbContext _dbContext, ISessionCacheHandler _cache, Microsoft.AspNetCore.SignalR.HubCallerContext context, ILogger _logger)
    {
        var playerId = SuperplayUserProvider.GetUserId(context, _cache, _logger);
        return await TryGetPlayer(playerId, _dbContext, _cache, context, _logger);
    }

    public static async Task<Player?> TryGetPlayer(Guid playerId, ApplicationDbContext _dbContext, ISessionCacheHandler _cache, Microsoft.AspNetCore.SignalR.HubCallerContext context, ILogger _logger)
    {
        var player = await _dbContext.Players.FindAsync(playerId);
        if (player == null)
        {
            _logger.LogWarning($"{nameof(TryGetPlayer)} The player with GUID={playerId} is not found in the database.");
            return null;
        }
        return player;
    }
}