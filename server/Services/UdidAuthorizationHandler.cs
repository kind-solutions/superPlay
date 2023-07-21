using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Superplay.Data;
using System.Linq;
using System;
using System.Threading.Tasks;

public class UdidAuthorizationHandler : AuthorizationHandler<UuidAuthorizationRequirement>
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly ApplicationDbContext _context;

    public UdidAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UuidAuthorizationRequirement requirement)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var username))
        {
            if (!_httpContextAccessor.HttpContext.Request.Query.TryGetValue("access_token", out username))
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }
        var playerid = username.ToString().Split(" ");
        if (playerid.Length != 2) // Bearer <token>
        {
            context.Fail();
            return Task.CompletedTask;
        }
        // // Not a guid
        if (!Guid.TryParse(playerid[1], out var id))
        {
            context.Fail();
            return Task.CompletedTask;
        }
        var player = _context.Players.Find(id);

        if (player == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        context.Succeed(requirement);
        
        // Return completed task  
        return Task.CompletedTask;
    }
}
