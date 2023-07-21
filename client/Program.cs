using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;
using Google.Protobuf.Examples.AddressBook;


var token = String.Empty;
var connection = new HubConnectionBuilder()
    .WithUrl("wss://localhost:7133/chatHub", HttpTransportType.WebSockets, options =>
    {
        options.AccessTokenProvider = () =>
        {
            return Task.FromResult(token);
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
connection.On<string, string>("ReceiveMessage", (user, message) =>
{

    var newMessage = $"ReceiveMessage: {user}: {message}";
    Console.WriteLine(newMessage);
});

var login = new TaskCompletionSource<bool>();

connection.On<string>("LoginResponse", async playerId =>
{
    token = playerId;
    Console.WriteLine($"LoginResponse received token={playerId}");

    //restart connection so the token is correctly set
    await connection.StopAsync();
    await connection.StartAsync();
    login.SetResult(true);
});

await connection.StartAsync();

await connection.SendAsync("Login", "1234");

await login.Task;

var p = new Person
{
    Name = "gigi",
    Id = 1,
    Email = "gigi@gigi.com"
};
while (true)
{
    try
    {
        await connection.SendAsync("SendMessage", "test", new byte[] { 1, 2, 3 });
        await Task.Delay(300);

    }
    catch (System.Exception e)
    {

        Console.WriteLine($"exception {e}");
    }
}
