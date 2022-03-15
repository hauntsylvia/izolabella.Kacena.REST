using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Classes.Structures.Requests
{
    internal class RequestWrapper
    {
        internal RequestWrapper(MethodInfo RouteTo, IEndpointContainer Endpoint, object? Payload)
        {
            this.RouteTo = RouteTo;
            this.Endpoint = Endpoint;
            this.Payload = Payload;
        }

        public MethodInfo RouteTo { get; }
        public IEndpointContainer Endpoint { get; }
        public object? Payload { get; }
        public object? Invoke()
        {
            return this.RouteTo.Invoke(null, this.Payload != null ? new object[] { this.Payload } : Array.Empty<object>());
        }
    }
}
