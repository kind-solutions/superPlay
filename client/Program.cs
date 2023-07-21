using Microsoft.AspNetCore.SignalR.Client;

var token = String.Empty;
var connection = new HubConnectionBuilder()
    .WithUrl("wss://localhost:7133/chatHub", Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets, options =>
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
    Console.WriteLine("Logged");
};

connection.On<string, string>("ReceiveMessage", (user, message) =>
{

    var newMessage = $"ReceiveMessage: {user}: {message}";
    Console.WriteLine(newMessage);
});

var t = new TaskCompletionSource<bool>();

connection.On<string>("LoginResponse", async playerId =>
{
    token = playerId;
    Console.WriteLine($"LoginResponse received token={playerId}");

    //restart connection so the token is correctly set
    await connection.StopAsync();
    await connection.StartAsync();
    t.SetResult(true);
});

await connection.StartAsync();

await connection.SendAsync("Login", "1234");

await t.Task;


while (true)
{
    try
    {
        await connection.SendAsync("SendMessage", "test", "msg");
        await Task.Delay(300);

    }
    catch (System.Exception e)
    {

        Console.WriteLine($"exception {e}");
    }
}
