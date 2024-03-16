using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoLib.PayloadTypes
{
    public interface IJsonPayload: IPayload
    {
        string GetJson();
    }
}
