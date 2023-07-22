using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Google.Protobuf;

using Superplay.Protobuf.Messages;

Player? LoggedInUser = null;
var deviceId = "0a9ca9a6-d52b-4072-baca-2e5a8a419ad6";

var connection = new HubConnectionBuilder()
    .WithUrl("wss://localhost:5001/chatHub", HttpTransportType.WebSockets, options =>
    {
        options.AccessTokenProvider = () =>
        {
            return Task.FromResult(deviceId);
        };
    })
    .Build();

connection.Closed += async (error) =>
{
    await Task.Delay(1);
    Console.WriteLine("Connection Closed");
};
connection.Reconnected += async (error) =>
{
    await Task.Delay(1);
    Console.WriteLine("Connection Reconnected");
};

connection.Reconnecting += async (error) =>
{
    await Task.Delay(1);
    Console.WriteLine("Connection Reconnecting");
};

var login = new TaskCompletionSource<bool>();

connection.On<byte[]>("LoginResponse", payload =>
{
    var response = LoginResponse.Parser.ParseFrom(payload);
    LoggedInUser = response.Myself;

    Console.WriteLine($"LoginResponse received token={LoggedInUser.Id}");

    //restart connection so the token is correctly set
    login.SetResult(true);
});

await connection.StartAsync();

var loginRequest = new LoginRequest
{
    Udid = "0a9ca9a6-d52b-4072-baca-2e5a8a419ad6",
};

await connection.SendAsync("Login", loginRequest.ToByteArray());

await login.Task;


var random = new Random();
while (true)
{
    try
    {

        var req = new UpdateResourcesRequest
        {
            Type = random.NextDouble() > 0.5f ? ResourceType.Coins : ResourceType.Rolls,
            Ammount = new ResourceValue { Value = random.Next(-100, 100) },
        };
        Console.WriteLine($"Sending {nameof(UpdateResourcesRequest)} {req} with size: {req.CalculateSize()}B");
        await connection.SendAsync("UpdateResources", req.ToByteArray());
        await Task.Delay(300);

    }
    catch (System.Exception e)
    {

        Console.WriteLine($"exception {e}");
    }
}
