using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

public class UdidAuthorizationHandler : AuthorizationHandler<UuidAuthorizationRequirement>
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public UdidAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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

        // // Not a guid

        // if (!Guid.TryParse(username, out _))
        // {
        //     context.Fail();
        //     return Task.CompletedTask;
        // }

        context.Succeed(requirement);

        // Return completed task  
        return Task.CompletedTask;
    }
}
