

using ProtoLib;
using ProtoLib.PayloadTypes;

ProtoMessage m = new ProtoMessage()
{
    Action = "auth",
};
m.SetHeader("type", "json");
//
m.SetPayload(new AuthRequestPayload("vasia", "123123123"));

