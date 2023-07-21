using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using Superplay.Data;
using Superplay.Models;

namespace Superplay.Authorization;
public class SuperplayUserProvider : IUserIdProvider
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public SuperplayUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId(HubConnectionContext connection)
    {
        return GetUserId(_httpContextAccessor);
    }

    public static string GetUserId(IHttpContextAccessor _httpContextAccessor)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return String.Empty;
        }

        if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var username))
        {
            if (!_httpContextAccessor.HttpContext.Request.Query.TryGetValue("access_token", out username))
            {
                return String.Empty;
            }
        }
        var playerid = username.ToString().Split(" ");
        if (playerid.Length != 2) // Bearer <token>
        {
            return String.Empty;
        }

        return playerid[1];
    }
    public static Guid GetUserId(IHttpContextAccessor _httpContextAccessor, ILogger _logger)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return Guid.Empty;
        }

        if (!_httpContextAccessor.HttpContext.Items.TryGetValue("player", out var data))
        {
            _logger.LogError($"{nameof(SuperplayAuthorizationHandler)} did not add playerData to HttpContext.");
            return Guid.Empty;
        }
        var id = data as string;

        if (id == null)
        {
            _logger.LogError($"{nameof(SuperplayAuthorizationHandler)} did not add a string as playerData to HttpContext.");
            return Guid.Empty;
        }

        if (!Guid.TryParse(id, out var playerId))
        {
            _logger.LogError($"The string added as playerData to HttpContext by {nameof(SuperplayAuthorizationHandler)} is not a GUID.");
            return Guid.Empty;
        }

        return playerId;
    }

    public static async Task<Player?> TryGetPlayer(ApplicationDbContext _dbContext, IHttpContextAccessor _httpContextAccessor, ILogger _logger)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            _logger.LogError($"{nameof(TryGetPlayer)} HttpContext is null.");
            return null;
        };

        var playerId = SuperplayUserProvider.GetUserId(_httpContextAccessor, _logger);
        var player = await _dbContext.Players.FindAsync(playerId);

        if (player == null)
        {
            _logger.LogWarning($"{nameof(TryGetPlayer)} The player with GUID={playerId} is not found in the database.");
            return null;
        }
        return player;
    }
}