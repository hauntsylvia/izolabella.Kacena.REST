using izolabella.Kacena.REST.Objects.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Objects.Structures.Requests
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<object?> Invoke()
        {
            if (this.RouteTo.GetParameters().Length == 0 || this.Payload != null)
            {
                object? Return = this.RouteTo.Invoke(this.Endpoint, this.Payload != null ? new object[] { this.Payload } : null);
                if (Return != null && this.RouteTo.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null)
                {
                    if (this.RouteTo.ReturnType.IsGenericType)
                        return await (dynamic)Return;
                }
                return Return;
            }
            else
            {
                return Errors.MissingPayload;
            }
        }
    }
}
