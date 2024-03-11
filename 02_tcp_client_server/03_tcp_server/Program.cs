



using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

const string host = "127.0.0.1";
const int port = 8080;

TcpListener? listener = null;

try
{
	listener = new TcpListener(IPAddress.Parse(host), port);
	listener.Start();

    Console.WriteLine($"Server started at {host}:{port}");

	TcpClient client = listener.AcceptTcpClient();

	using NetworkStream stream = client.GetStream();
	using StreamReader reader = new StreamReader(stream);
	using StreamWriter writer = new StreamWriter(stream);

	string? message = reader.ReadToEnd();
    Console.WriteLine($">>> {message}");

	writer.WriteLine("Hello from server");
	stream.Flush();
}
catch (Exception ex)
{

    Console.WriteLine($"ERROR: {ex.Message}");
}
finally
{
	listener?.Stop();
}
