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
        public required string? Action { get; set; }
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

            Headers[HEADER_PAYLOAD_LEN] = PayloadStream.Length.ToString();



            Headers[HEADER_PAYLOAD_TYPE]
        }

        public MemoryStream GetStream()
        {
            MemoryStream? payloadStream = null;

            if (Payload is not null)
            {
                payloadStream = Payload.GetStream();
                Headers["len"] = 
            }





        }



    }
}
