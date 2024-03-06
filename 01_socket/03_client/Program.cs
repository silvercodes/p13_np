

using System.Net.Sockets;
using System.Net;
using System.Text;

const string serverIp = "127.0.0.1";
const int port = 8080;


Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), port);

try
{
    Console.Write("> ");
    string? message = Console.ReadLine();

    if (message is null)
        throw new ArgumentException("Message is empty");

    socket.Connect(serverEndPoint);                     // BLOCKING
    socket.Send(Encoding.UTF8.GetBytes(message));       // BLOCKING

    byte[] buffer = new byte[1024];
    int byteCount = 0;
    string response = string.Empty;
    do
    {
        byteCount = socket.Receive(buffer);                   // BLOCKING	
        response += Encoding.UTF8.GetString(buffer, 0, byteCount);
    } while (socket.Available > 0);

    Console.WriteLine($"Response: {response}");

    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
catch (Exception ex)
{

    Console.WriteLine($"Error: {ex.Message}");
}

Console.ReadLine();


