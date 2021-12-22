using kacena.Classes.Interfaces.Attributes.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Attributes.HTTP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HTTPPut : Attribute, IHTTPAttribute
    {
        public string verb => "PUT";
    }
}
