using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using SignalRChat.Hubs;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;

using Superplay.Data;

var builder = WebApplication.CreateBuilder(args);
var DbPath = "super.db";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={DbPath}"));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddScoped<IAuthorizationHandler, UdidAuthorizationHandler>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserIdProvider, UdidBasedUserIdProvider>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomHubAuthorizatioPolicy", policy =>
    {
        policy.Requirements.Add(new UuidAuthorizationRequirement());
    });
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Host.UseSerilog();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        DbInitializer.Initialize(services);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub", options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.Run();

