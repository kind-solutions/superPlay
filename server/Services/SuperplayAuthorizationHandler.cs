using Microsoft.AspNetCore.Authorization;
using Superplay.Data;

namespace Superplay.Authorization;
public class SuperplayAuthorizationHandler : AuthorizationHandler<UuidAuthorizationRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;
    private readonly ISessionCacheHandler _cache;
    public SuperplayAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, ISessionCacheHandler cache)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _cache = cache;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UuidAuthorizationRequirement requirement)
    {
        var deviceId = SuperplayUserProvider.GetDeviceId(_httpContextAccessor);


        if (!_cache.IsDeviceCached(deviceId))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        // Return completed task  
        return Task.CompletedTask;
    }
}
