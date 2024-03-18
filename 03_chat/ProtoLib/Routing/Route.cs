using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoLib.Routing;

public class Route
{
    public string ActionString { get; set; }
    public Type ControllerType;
    public string action;


    public void Execute(ProtoMessage pm)
    {
        object? controller = Activator.CreateInstance(ControllerType);

        ControllerType.GetMethod(action).Invoke(controller, new[] { pm });


    }



}
