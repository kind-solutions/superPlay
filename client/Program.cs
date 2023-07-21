using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Hello, World!");

using (ClientWebSocket ws = new ClientWebSocket())
{
    var serverUri = new Uri("wss://localhost:7133/ws");

    //Implementation of timeout of 5000 ms
    var source = new CancellationTokenSource();
    source.CancelAfter(5000);

    await ws.ConnectAsync(serverUri, source.Token);
    var iterationNo = 0;
    // restricted to 5 iteration only
    while (ws.State == WebSocketState.Open && iterationNo++ < 5)
    {
        string msg = "hello0123456789123456789123456789123456789123456789123456789";
        ArraySegment<byte> bytesToSend =
                    new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
        await ws.SendAsync(bytesToSend, WebSocketMessageType.Text,
                             true, source.Token);
        //Receive buffer
        var receiveBuffer = new byte[200];
        //Multipacket response
        var offset = 0;
        var dataPerPacket = 10; //Just for example
        while (true)
        {
            ArraySegment<byte> bytesReceived =
                      new ArraySegment<byte>(receiveBuffer, offset, dataPerPacket);
            WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived,
                                                          source.Token);
            //Partial data received
            Console.WriteLine("Data:{0}",
                             Encoding.UTF8.GetString(receiveBuffer, offset,
                                                          result.Count));
            offset += result.Count;
            if (result.EndOfMessage)
                break;
        }
        Console.WriteLine("Complete response: {0}",
                            Encoding.UTF8.GetString(receiveBuffer, 0,
                                                        offset));

    }
    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

}