using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Classes.Structures.Requests
{
    internal class RequestWrapper<T>
    {
        internal RequestWrapper(MethodInfo RouteTo, T? Payload)
        {
            this.RouteTo = RouteTo;
            this.Payload = Payload;
        }

        public MethodInfo RouteTo { get; }
        public T? Payload { get; }
    }
}
