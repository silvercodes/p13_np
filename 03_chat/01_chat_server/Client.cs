﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_chat_server
{
    internal class Client
    {
        private TcpClient tcpClient;
        private NetworkStream netStream = null!;

        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        public void Processing()
        {
            try
            {
                netStream = tcpClient.GetStream();

                // test code
                while(true)
                {
                    MemoryStream memStream = Read(netStream);
                    memStream.Position = 0;

                    using StreamReader sr = new StreamReader(memStream);
                    string data = sr.ReadToEnd();
                    Console.WriteLine(data);

                }



            }
            catch (Exception)
            {

                throw;
            }
        }


        private MemoryStream Read(NetworkStream netStream)
        {
            MemoryStream memoryStream = new MemoryStream();

            byte[] bytes = new byte[1024];
            int readBytesCount = 0;
            readBytesCount = netStream.Read(bytes, 0, bytes.Length);
            memoryStream.Write(bytes, 0, readBytesCount);

            return memoryStream;
        }


    }


}
