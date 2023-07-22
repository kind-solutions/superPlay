//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;

using Superplay.Hubs;
using Superplay.Data;
using Superplay.Authorization;
using Superplay.Services;

var builder = WebApplication.CreateBuilder(args);
var DbPath = "super.db";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={DbPath}"));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddScoped<IAuthorizationHandler, SuperplayAuthorizationHandler>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserIdProvider, SuperplayUserProvider>();
builder.Services.AddSingleton<ISessionCacheHandler, SessionCacheHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomHubAuthorizatioPolicy", policy =>
    {
        policy.Requirements.Add(new SuperplayAuthorizationRequirement());
    });
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Host.UseSerilog();

if (builder.Environment.IsDevelopment())
{
    // builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
app.MapHub<EconomyHub>("/chatHub", options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.Run();

