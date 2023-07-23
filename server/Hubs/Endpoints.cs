
using Microsoft.AspNetCore.Http.Connections;

namespace Superplay.Hubs;

class Hubs
{
    public static void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<EconomyHub>("/economy", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
            });
    }
}



