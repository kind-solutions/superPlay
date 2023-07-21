using Microsoft.AspNetCore.Authorization;
using Superplay.Data;

namespace Superplay.Authorization;
public class SuperplayAuthorizationHandler : AuthorizationHandler<UuidAuthorizationRequirement>
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly ApplicationDbContext _context;

    private static HashSet<Guid> playerCache = new();
    public SuperplayAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UuidAuthorizationRequirement requirement)
    {

        var playerId = SuperplayUserProvider.GetUserId(_httpContextAccessor);

        if (_httpContextAccessor.HttpContext == null || !Guid.TryParse(playerId, out var id))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (!playerCache.TryGetValue(id, out _))
        {
            var player = _context.Players.Find(id);
            if (player == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            playerCache.Add(player.Id);
        }


        _httpContextAccessor.HttpContext.Items["player"] = playerId;

        context.Succeed(requirement);
        // Return completed task  
        return Task.CompletedTask;
    }
}
