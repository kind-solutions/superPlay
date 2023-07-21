using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("wss://localhost:7133/chatHub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult("playerid");
    })
    .Build();

connection.Closed += async (error) =>
{
    await Task.Delay(new Random().Next(0, 5) * 1000);
    await connection.StartAsync();
};

connection.On<string, string>("ReceiveMessage", (user, message) =>
    {

        var newMessage = $"ReceiveMessage: {user}: {message}";
        Console.WriteLine(newMessage);
    });

await connection.StartAsync();

Console.WriteLine("username: ");
var user = Console.ReadLine();

while (true)
{
    Console.WriteLine("message: ");
    var msg = Console.ReadLine();
    await connection.InvokeAsync("SendMessage", user, msg);
}
