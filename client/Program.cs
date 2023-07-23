//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Google.Protobuf;
using Serilog;
using Superplay.Protobuf.Messages;

Log.Logger = new LoggerConfiguration()
                          // add console as logging target
                          .WriteTo.Console()
                          // set default minimum level
                          .MinimumLevel.Debug()
                          .CreateLogger();



var deviceId = String.Empty;
do
{
    Console.Write("your device id: ");
    deviceId = Console.ReadLine();

} while (!Guid.TryParse(deviceId, out _));

var Initialized = false;

var connection = new HubConnectionBuilder()
    .WithUrl("wss://localhost:7133/economy", options =>
    {
        options.AccessTokenProvider = () =>
        {
#pragma warning disable CS8619 //string is not string?
            return Task.FromResult(deviceId);

        };

        options.Transports = HttpTransportType.WebSockets;
        options.SkipNegotiation = true;
    })
    .WithAutomaticReconnect()
    .Build();

connection.Closed += async (error) =>
{
    await Task.Delay(1);
    Initialized = false;
    Log.Information("Connection Closed");
};
connection.Reconnected += async (error) =>
{
    Initialized = true;
    await Task.Delay(1);
    Log.Information("Connection Reconnected");
};

connection.Reconnecting += async (error) =>
{
    Initialized = false;
    await Task.Delay(1);
    Log.Information("Connection Reconnecting");
};

connection.On("RequestLogin", async () =>
{
    Initialized = false;
    await connection.SendAsync("Login");
});

connection.On<byte[]>("LoginResponse", async payload =>
{
    var response = LoginResponse.Parser.ParseFrom(payload);

    if (response.Myself == null)
    {
        Log.Error($"I failed to login, will close connection");
        Initialized = false;
        await connection.StopAsync();
        throw new UnauthorizedAccessException($"Login Failed");
    }
    Initialized = true;

    Log.Information($"Device logged in, I am {response.Myself.Id}");

    //restart connection so the token is correctly set
});

connection.On<byte[]>("GiftEvent", payload =>
{
    var response = GiftEvent.Parser.ParseFrom(payload);

    if (response == null || response.From == null || response.From.Id == null || response.Ammount == null)
    {
        Log.Warning($"Received GiftEvent with bad data");
        return;
    }
    Log.Debug($"Received GiftEvent {response} with size {response.CalculateSize()}B");

});

await connection.StartAsync();

var random = new Random();
while (true)
{
    if (!Initialized) continue;
    try
    {
        var coinFlip = random.NextDouble() > 0.5f;
        var anotherCoinFlip = random.NextDouble() > 0.5f;

        if (coinFlip)
        {
            var req = new SendGiftRequest
            {
                FriendId = "674cb2bf-8288-4c7b-b1f2-3c8701c37eca",
                Ammount = new ResourceValue { Value = random.Next(1, 10) },
                Type = anotherCoinFlip ? ResourceType.Coins : ResourceType.Rolls,
            };

            Log.Debug($"Sending {nameof(SendGiftRequest)} {req} with size: {req.CalculateSize()}B");
            await connection.SendAsync("SendGift", req.ToByteArray());
        }
        else
        {
            var req = new UpdateResourcesRequest
            {
                Type = anotherCoinFlip ? ResourceType.Coins : ResourceType.Rolls,
                Ammount = new ResourceValue { Value = random.Next(-100, 100) },
            };
            Log.Debug($"Sending {nameof(UpdateResourcesRequest)} {req} with size: {req.CalculateSize()}B");
            await connection.SendAsync("UpdateResources", req.ToByteArray());
        }

        await Task.Delay(300);

    }
    catch (System.Exception e)
    {

        Log.Error($"exception {e}");
    }
}
