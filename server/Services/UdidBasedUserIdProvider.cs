using Microsoft.AspNetCore.SignalR;


public class UdidBasedUserIdProvider : IUserIdProvider
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public UdidBasedUserIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId(HubConnectionContext connection)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return String.Empty;
        }

        var id = _httpContextAccessor.HttpContext.Request.Query["username"].ToString();

        return id;
    }
}