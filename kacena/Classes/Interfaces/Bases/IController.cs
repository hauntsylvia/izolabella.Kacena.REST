using kacena.Classes.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Interfaces.Bases
{
    public interface IController
    {
        string serviceName { get; }
        API api { get; }
    }
}
