//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

namespace Superplay.Services;

// DEMO IMPLEMENTATION
// PROD should be based on redis or other in mem db service
class SessionCacheHandler : ISessionCacheHandler
{

    private Dictionary<Guid, string> playerCache = new();
    private Dictionary<Guid, string> deviceCache = new();

    private Dictionary<string, Guid> connectionToPlayer = new();
    private Dictionary<string, Guid> connectionToDevice = new();

    public bool IsPlayerCached(Guid id)
    {
        return playerCache.TryGetValue(id, out _);
    }

    public bool IsDeviceCached(Guid id)
    {
        return deviceCache.TryGetValue(id, out _); ;
    }

    public bool TryGetPlayerConnection(Guid id, out string? connection)
    {
        return playerCache.TryGetValue(id, out connection);
    }

    public bool TryGetDeviceConnection(Guid id, out string? connection)
    {
        return deviceCache.TryGetValue(id, out connection);
    }

    public bool CacheSession(string connection, Guid device, Guid player)
    {
        var success = true;


        success &= CachePlayerConnection(player, connection);
        success &= CacheDeviceConnection(device, connection);

        return success;
    }
    public bool TryGetSession(string connection, out (Guid, Guid) session)
    {
        if (connectionToPlayer.TryGetValue(connection, out var player))
        {
            if (connectionToDevice.TryGetValue(connection, out var device))
            {
                session = (device, player);
                return true;
            }
        }
        session = (Guid.Empty, Guid.Empty);
        return false;

    }
    public bool DeleteSession(string connection)
    {
        if (connectionToPlayer.TryGetValue(connection, out var player))
        {
            playerCache.Remove(player);
            connectionToPlayer.Remove(connection);
        }

        if (connectionToDevice.TryGetValue(connection, out var device))
        {
            deviceCache.Remove(device);
            connectionToDevice.Remove(connection);
        }
        return true;
    }
    public bool CachePlayerConnection(Guid player, string connection)
    {
        var success = true;
        success &= playerCache.TryAdd(player, connection);
        if (success)
        {
            success &= connectionToPlayer.TryAdd(connection, player);
        }
        return success;
    }

    public bool CacheDeviceConnection(Guid device, string connection)
    {
        var success = true;
        success &= deviceCache.TryAdd(device, connection);
        if (success)
        {
            success &= connectionToDevice.TryAdd(connection, device);
        }

        return success;

    }
    public bool TryGetPlayerFromConnection(string connection, out Guid player)
    {
        if (connectionToPlayer.TryGetValue(connection, out player))
        {
            return true;
        }
        player = Guid.Empty;
        return false;
    }
    public bool TryGetDeviceFromConnection(string connection, out Guid device)
    {
        if (connectionToDevice.TryGetValue(connection, out device))
        {
            return true;
        }
        device = Guid.Empty;
        return false;

    }

}