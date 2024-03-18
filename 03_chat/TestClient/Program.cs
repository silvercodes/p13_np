

using ProtoLib;
using ProtoLib.PayloadTypes;
using System.Net.Sockets;


const string host = "127.0.0.1";
const int port = 8080;

using TcpClient tcpClient = new TcpClient(host, port);
using NetworkStream netStrteam = tcpClient.GetStream();


ProtoMessage m = new ProtoMessage()
{
    Action = "auth",
};
m.SetHeader("testHeader", "hohoho");
//
m.SetPayload(new AuthRequestPayload("vasia", "123123123"));


try
{
    while(true)
    {
        MemoryStream memStream = m.GetStream();
        Console.Write("Press to send");
        Console.ReadLine();
        Console.WriteLine(memStream.Length);
        memStream.CopyTo(netStrteam);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}






Console.ReadLine();