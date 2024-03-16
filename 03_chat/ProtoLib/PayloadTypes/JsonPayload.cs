using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoLib.PayloadTypes
{
    public abstract class JsonPayload: IPayload
    {
        public abstract MemoryStream GetStream();
        public string GetPayloadType()
        {
            return "json";
        }
        public abstract string GetJson();
    }
}
