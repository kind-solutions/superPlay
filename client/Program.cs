using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Google.Protobuf;

using Superplay.Protobuf.Messages;

var deviceId = String.Empty;
do
{
    deviceId = Console.ReadLine();

} while (!Guid.TryParse(deviceId, out _));


var connection = new HubConnectionBuilder()
    .WithUrl("wss://localhost:5001/chatHub", options =>
    {
        options.AccessTokenProvider = () =>
        {
            #pragma warning disable CS8619 //string is not string?
            return Task.FromResult(deviceId);
        };
        options.Transports = HttpTransportType.WebSockets;
        options.SkipNegotiation = true;
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

    if (response.Myself == null)
    {
        Console.WriteLine($"I failed to login, will crash");
        throw new UnauthorizedAccessException($"Login Failed");
    }

    Console.WriteLine($"Device logged in, I am {response.Myself.Id}");

    //restart connection so the token is correctly set
    login.SetResult(true);
});

connection.On<byte[]>("GiftEvent", payload =>
{
    var response = GiftEvent.Parser.ParseFrom(payload);

    if (response == null || response.From == null || response.From.Id == null ||  response.Ammount == null)
    {
        Console.WriteLine($"Received GiftEvent with bad data");
        return;
    }
    Console.WriteLine($"Received GiftEvent {response}");

});

await connection.StartAsync();

await connection.SendAsync("Login");

await login.Task;


var random = new Random();
while (true)
{
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

            Console.WriteLine($"Sending {nameof(SendGiftRequest)} {req} with size: {req.CalculateSize()}B");
            await connection.SendAsync("SendGift", req.ToByteArray());
        }
        else
        {
            var req = new UpdateResourcesRequest
            {
                Type = anotherCoinFlip ? ResourceType.Coins : ResourceType.Rolls,
                Ammount = new ResourceValue { Value = random.Next(-100, 100) },
            };
            Console.WriteLine($"Sending {nameof(UpdateResourcesRequest)} {req} with size: {req.CalculateSize()}B");
            await connection.SendAsync("UpdateResources", req.ToByteArray());
        }

        await Task.Delay(300);

    }
    catch (System.Exception e)
    {

        Console.WriteLine($"exception {e}");
    }
}
