using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Classes.Structures.Requests
{
    internal class RequestWrapper
    {
        internal RequestWrapper(MethodInfo RouteTo, object EndpointContainer, IEndpointContainer Endpoint, object? Payload)
        {
            this.RouteTo = RouteTo;
            this.EndpointContainer = EndpointContainer;
            this.Endpoint = Endpoint;
            this.Payload = Payload;
        }

        public MethodInfo RouteTo { get; }
        public object EndpointContainer { get; }
        public IEndpointContainer Endpoint { get; }
        public object? Payload { get; }
        public async Task<object?> Invoke()
        {
            try
            {
                object? Return = this.RouteTo.Invoke(this.EndpointContainer, this.Payload != null ? new object[] { this.Payload } : null);
                if (Return != null && this.RouteTo.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null)
                    if (this.RouteTo.ReturnType.IsGenericType)
                        return await (dynamic)Return;
                return Return;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                throw;
            }
        }
    }
}
