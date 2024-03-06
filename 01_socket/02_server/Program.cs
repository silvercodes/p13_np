



using System.Net;
using System.Net.Sockets;
using System.Text;

const string serverIp = "127.0.0.1";
const int port = 8080;


using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(serverIp), port);

try
{
	socket.Bind(endpoint);
	socket.Listen();

    Console.WriteLine($"Server started at {serverIp}:{port}");

	while(true)
	{
		Socket remoteSocket = socket.Accept();              // BLOCKING
        Console.WriteLine("Connection opened...");


		byte[] buffer = new byte[1024];
		int byteCount = 0;
		string message = string.Empty;
		do
		{
			byteCount = remoteSocket.Receive(buffer);                   // BLOCKING	
			message += Encoding.UTF8.GetString(buffer, 0, byteCount);
		} while (remoteSocket.Available > 0);

        Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {message}");

		Thread.Sleep(2000);
		string response = "Hello from server! All OK!";
//		string response = @"HTTP/1.1 200 OK
//Date: Thu, 29 Jul 2021 19:20:01 GMT
//Content-Type: text/html; charset=utf-8
//Connection: close
//Server: gunicorn/19.9.0
//Access-Control-Allow-Origin: *
//Access-Control-Allow-Credentials: true

//<h1 style='color:red'>Vasia</h1>
//";
		remoteSocket.Send(Encoding.UTF8.GetBytes(response));

		remoteSocket.Shutdown(SocketShutdown.Both);
		remoteSocket.Close();

        Console.WriteLine("Connection closed...");


    }
}
catch (Exception ex)
{

    Console.WriteLine($"Error: {ex.Message}");
}








