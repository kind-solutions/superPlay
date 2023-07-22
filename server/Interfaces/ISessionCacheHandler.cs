using Superplay.Models;

public interface ISessionCacheHandler
{
    public bool IsPlayerCached(Guid id);
    public bool IsDeviceCached(Guid id);
    public bool IsConnectionCached(string id);

    public bool TryGetPlayerConnection(Guid id, out string? connection);
    public bool TryGetDeviceConnection(Guid id, out string? connection);

    public bool CacheSession(string connection, Guid device, Guid player);
    public bool TryGetSession(string connection, out (Guid, Guid) session);
    public bool DeleteSession(string connection);

    public bool CachePlayerConnection(Guid player, string connection);
    public bool CacheDeviceConnection(Guid device, string connection);

    public bool TryGetPlayerFromConnection(string connection, out Guid player);
    public bool TryGetDeviceFromConnection(string connection, out Guid device);
}