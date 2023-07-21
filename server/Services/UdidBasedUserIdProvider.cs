using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;


public class UdidBasedUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(ClaimTypes.Sid)?.Value!;
    }
}