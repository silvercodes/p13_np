using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProtoLib.Core;

public class ProtoMessageBuilder
{
    private NetworkStream netStrteam;
    private MemoryStream memStream;

    public ProtoMessageBuilder(NetworkStream netStrteam)
    {
        this.netStrteam = netStrteam;
    }

    public ProtoMessage Receive()
    {
        int readingSize = ConvertToInt(ReadBytes(4));

        memStream = new MemoryStream(400);
        Read(400);







        return new ProtoMessage();
    }

    private byte[] ReadBytes(int count)
    {
        
        byte[] bytes = new byte[count];
        netStrteam.ReadExactly(bytes, 0, count);

        return bytes;
    }

    private void Read(int count)
    {
        byte[] bytes = new byte[count];
        netStrteam.ReadExactly(bytes, 0, count);

        memStream.Write(bytes, 0, count);
    }

    private int ConvertToInt(byte[] bytes)
    {
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return BitConverter.ToInt32(bytes, 0);
    }
}
