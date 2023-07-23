//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using Serilog;
using Microsoft.AspNetCore.Authorization;

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
builder.Host.UseSerilog();


builder.Services.AddScoped<IAuthorizationHandler, SuperplayAuthorizationHandler>();

builder.Services.AddSignalR();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserIdProvider, SuperplayUserProvider>();
builder.Services.AddSingleton<ISessionCacheHandler, SessionCacheHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperplayAuthorizationPolicy", policy =>
    {
        policy.Requirements.Add(new SuperplayAuthorizationRequirement());
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        DbInitializer.Initialize(services);
    }
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    Hubs.ConfigureEndpoints(endpoints);
});

app.Run();

