//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

using Microsoft.AspNetCore.Authorization;
using Superplay.Data;

namespace Superplay.Authorization;

// FOR DEMO PURPOSES ONLY
// in PROD it should be separated Identity service
public class SuperplayAuthorizationHandler : AuthorizationHandler<SuperplayAuthorizationRequirement>
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
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SuperplayAuthorizationRequirement requirement)
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
