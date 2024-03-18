using ProtoLib;
using ProtoLib.Core;
using System;
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

                ProtoMessageBuilder builder = new ProtoMessageBuilder(netStream);


                while (true)
                {
                    ProtoMessage protoMessage = builder.Receive();


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
