using ProtoLib.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProtoLib
{
    public class ProtoMessage
    {
        private const string HEADER_PAYLOAD_LEN = "len";
        private const string HEADER_PAYLOAD_TYPE = "ptype";
        public string? Action { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public IPayload? Payload { get; private set; }
        private MemoryStream? PayloadStream { get; set; }



        public void SetHeader(string key, string value)
        {
            Headers[key] = value;
        }

        public void SetPayload(IPayload payload)
        {
            Payload = payload;

            PayloadStream = payload.GetStream();
            PayloadStream.Position = 0;

            Headers[HEADER_PAYLOAD_LEN] = PayloadStream.Length.ToString();
            Headers[HEADER_PAYLOAD_TYPE] = Payload.GetPayloadType();
        }

        public MemoryStream GetStream()
        {
            MemoryStream memStream = new MemoryStream();
            memStream.Write(new byte[4], 0, 4);

            StreamWriter writer = new StreamWriter(memStream);

            writer.WriteLine(Action);
            foreach (KeyValuePair<string, string> h in Headers)
                writer.WriteLine($"{h.Key}:{h.Value}");
            writer.WriteLine();
            writer.Flush();

            if (Payload is not null)
            {
                PayloadStream.CopyTo(memStream);
            }

            memStream.Position = 0;

            byte[] sizeHeader = ConvertInt((int)memStream.Length - 4);
            memStream.Write(sizeHeader, 0, 4);

            memStream.Position = 0;

            return memStream;
        }

        private byte[] ConvertInt(int val)
        {
            byte[] intBytes = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);

            return intBytes;
        }
    }
}
