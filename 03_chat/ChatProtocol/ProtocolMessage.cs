using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProtocol
{
    public class ProtocolMessage<T>
    {
        public ProtocolMessageType ProtocolMessageType { get; set; }
        public bool IsSuccessed { get; set; } = true;
        public T? Payload { get; set; }
    }
}
