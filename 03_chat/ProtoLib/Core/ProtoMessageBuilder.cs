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
    private MemoryStream memStream = null!;

    public ProtoMessageBuilder(NetworkStream netStrteam)
    {
        this.netStrteam = netStrteam;
    }

    public ProtoMessage Receive()
    {
        int readingSize = ConvertToInt(ReadBytes(4));

        memStream = new MemoryStream(readingSize);
        memStream.Write(ReadBytes(readingSize), 0, readingSize);
        memStream.Position = 0;

        ProtoMessage pm  = new ProtoMessage();
        
        using StreamReader sr = new StreamReader(memStream);

        ExtractMetadata(pm, sr);
        ExtrtactpayloadStrream(pm);

        memStream.Dispose();

        return pm;
    }

    private void ExtractMetadata(ProtoMessage pm, StreamReader sr)
    {
        sr.BaseStream.Position = 0;

        pm.Action = sr.ReadLine();

        string headerLine;
        while(! string.IsNullOrEmpty(headerLine = sr.ReadLine()))
            pm.SetHeader(headerLine);
    }

    private void ExtrtactpayloadStrream(ProtoMessage pm)
    {
        int payloadLength = pm.PayloadLength;

        memStream.Seek(-payloadLength, SeekOrigin.End);

        MemoryStream payloadStream = new MemoryStream(payloadLength);
        memStream.CopyTo(payloadStream);
        payloadStream.Position = 0;

        pm.PayloadStream = payloadStream;
    }

    private byte[] ReadBytes(int count)
    {
        
        byte[] bytes = new byte[count];
        netStrteam.ReadExactly(bytes, 0, count);

        return bytes;
    }

    private int ConvertToInt(byte[] bytes)
    {
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return BitConverter.ToInt32(bytes, 0);
    }
}
