using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProtoLib.PayloadTypes
{
    public interface IPayload
    {
        public MemoryStream GetStream();
        public string GetPayloadType();
    }
}
