using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_chat_server
{
    internal class Server
    {
        private string host;
        private int port;

        private TcpListener listener;

        private List<Client> clients = new List<Client>();

        public Server(string host = "127.0.0.1", int port = 8080)
        {
            this.host = host;
            this.port = port;

            listener = new TcpListener(IPAddress.Parse(host), port);
        }

        public async Task Start()
        {
            try
            {
                listener.Start();
                await Console.Out.WriteLineAsync($"Server started at {host}:{port}");

                while (true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();

                    Client client = new Client(tcpClient);

                    clients.Add(client);

                    // TODO: set events handlers ???

                    _ = Task.Run(() => client.Processing());
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"ERROR: {ex.Message}");

            }
        }









    }
}
