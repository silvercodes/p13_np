using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _05_server_multi_threads
{
    internal class Server: IAsyncDisposable
    {
        private int backlog;
        public string Ip { get; }
        public int Port { get; }
        public IPEndPoint iPEndPoint { get; set; }
        public Socket ServerSocket { get; set; }

        public Server(string ip, int port, int backlog = 10)
        {
            Ip = ip;
            Port = port;
            this.backlog = backlog;

            iPEndPoint = new IPEndPoint(IPAddress.Parse(Ip), port);

            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(iPEndPoint);
        }

        public async Task StartAsync()
        {
            try
            {
                ServerSocket.Listen(backlog);
                Console.WriteLine($"Server started at {Ip}:{Port}");

                await HandleAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task HandleAsync()
        {
            while (true)
            {
                Socket remoteSocket = await ServerSocket.AcceptAsync();

                if (remoteSocket.RemoteEndPoint is IPEndPoint remoteEP)
                {
                    await Console.Out.WriteLineAsync($"Connection opened for remote --> {remoteEP.Address}:{remoteEP.Port}");
                }

                _ = Task.Run(() => HandleRequest(remoteSocket));
            }
        }

        private void HandleRequest(Socket remoteSocket)
        {
            byte[] buffer = new byte[1024];
            int byteCount = 0;
            string message = string.Empty;
            do
            {
                byteCount = remoteSocket.Receive(buffer);
                message += Encoding.UTF8.GetString(buffer, 0, byteCount);
            } while (remoteSocket.Available > 0);

            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {message}");

            Thread.Sleep(2000);
            string response = "Hello from server! All OK!";

            remoteSocket.Send(Encoding.UTF8.GetBytes(response));

            remoteSocket.Shutdown(SocketShutdown.Both);
            remoteSocket.Close();

            Console.WriteLine("Connection closed...");
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() =>
            {
                if (ServerSocket != null)
                {
                    ServerSocket.Dispose();
                }
            });
        }
    }
}
