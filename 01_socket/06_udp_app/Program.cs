using System.Net;
using System.Net.Sockets;
using System.Text;


const string localHost = "172.20.10.13";
const string remoteHost = "172.20.10.13";

Console.Write("Enter a local port: ");
int localPort = Int32.Parse(Console.ReadLine());
Console.Write("Enter a remote port: ");
int remotePort = Int32.Parse(Console.ReadLine());


Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

_ = Task.Run(() =>
{
	try
	{
		socket.Bind(new IPEndPoint(IPAddress.Parse(localHost), localPort));

		while(true)
		{
			byte[] buffer = new byte[65535];                // Размер буфера МАКСИМАЛЬНЫЙ!!!
			int byteCount = 0;
			string message = string.Empty;

			EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

			do
			{
				byteCount = socket.ReceiveFrom(buffer, ref remoteEP);
				message += Encoding.UTF8.GetString(buffer, 0, byteCount);
            } while (socket.Available > 0);


			if (remoteEP is IPEndPoint remoteEPWithInfo)
			{
				Console.Write($"FROM: {remoteEPWithInfo.Address}:{remoteEPWithInfo.Port}");
			}

            Console.WriteLine($" > {DateTime.Now.ToShortDateString()}: {message}");
        }
	}
	catch (Exception ex)
	{
        Console.WriteLine($"ERROR: {ex.Message}");
    }
	finally
	{
		socket.Shutdown(SocketShutdown.Both);
		socket.Close();
	}
});


try
{
    while(true)
	{
		Console.Write(" <<< ");
		string? message = Console.ReadLine();

		if (message is not null)
		{
			byte[] data = Encoding.UTF8.GetBytes(message);

			socket.SendTo(data, new IPEndPoint(IPAddress.Parse(remoteHost), remotePort));
		}
	}
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}
finally
{
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}



