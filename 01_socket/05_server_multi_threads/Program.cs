
//using System.Net;
//using System.Net.Sockets;
//using System.Text;

//const string serverIp = "127.0.0.1";
//const int port = 8080;


//using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(serverIp), port);

//try
//{
//    socket.Bind(endpoint);
//    socket.Listen(10);
//    Console.WriteLine($"Server started at {serverIp}:{port}");

//    await RunListenAsync();
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Error: {ex.Message}");
//}

//async Task RunListenAsync()
//{
//    while(true)
//    {
//        Socket remoteSocket = await socket.AcceptAsync();

//        if (remoteSocket.RemoteEndPoint is IPEndPoint remoteEP)
//        {
//            await Console.Out.WriteLineAsync($"Connection opened for remote --> {remoteEP.Address}:{remoteEP.Port}");
//        }

//        _ = Task.Run(() => HandleRequest(remoteSocket));
//    }
//}

//void HandleRequest(Socket remoteSocket)
//{
//    byte[] buffer = new byte[1024];
//    int byteCount = 0;
//    string message = string.Empty;
//    do
//    {
//        byteCount = remoteSocket.Receive(buffer);
//        message += Encoding.UTF8.GetString(buffer, 0, byteCount);
//    } while (remoteSocket.Available > 0);

//    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {message}");

//    Thread.Sleep(2000);
//    string response = "Hello from server! All OK!";

//    remoteSocket.Send(Encoding.UTF8.GetBytes(response));

//    remoteSocket.Shutdown(SocketShutdown.Both);
//    remoteSocket.Close();

//    Console.WriteLine("Connection closed...");
//}








using _05_server_multi_threads;

const string serverIp = "127.0.0.1";
const int port = 8080;

await using Server server = new Server(serverIp, port);
await server.StartAsync();





