using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using izolabella.Kacena.REST.Objects.Structures.Requests;
using izolabella.Kacena.REST.Objects.Attributes;
using izolabella.Kacena.REST.Objects.Structures;
using izolabella.Kacena.REST.Objects.Constants;

namespace izolabella.Kacena.REST.Objects.Util
{
    internal class RequestSurgeon
    {
        public static object? ResolvePayload<T>(HttpListenerContext Context)
        {
            T? Payload = default;
            if (Context.Request.ContentType != null && Context.Request.ContentType == "application/json" && Context.Request.InputStream != null && Context.Request.InputStream.CanRead)
            {
                using StreamReader ContextReader = new(Context.Request.InputStream);
                try
                {
                    Payload = JsonConvert.DeserializeObject<T>(ContextReader.ReadToEnd());
                }
                catch (JsonSerializationException)
                {
                    return Errors.WrongEntity;
                }
            }
            else
            {
                _ = new QueryStringConverter<T>(Context.Request.QueryString).TryConvert(out Payload);
            }
            return Payload;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of object that is expected within requests such as POST requests.</typeparam>
        /// <param name="Context"></param>
        /// <returns></returns>
        internal static RequestWrapper? CutRequest(HttpListenerContext Context, params IEndpointContainer[] EndpointContainers)
        {
            if (Context.Request.Url != null)
            {
                string[] Parts = Context.Request.Url.AbsolutePath.Split('/');
                if (Parts.Length > 2)
                {
                    string RequestedEndpoint = Parts[1];
                    string RequestedMethod = Parts[2];
                    foreach (IEndpointContainer EndpointContainer in EndpointContainers)
                    {
                        if (RequestedEndpoint.ToLower() == EndpointContainer.Route.ToLower().Trim('/'))
                        {
                            foreach (MethodInfo Method in EndpointContainer.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                            {
                                if (Method.Name.ToLower() == RequestedMethod.ToLower() && Method.GetCustomAttribute<KacenaEndpointAttribute>() != null)
                                {
                                    ParameterInfo? Parameter = Method.GetParameters().FirstOrDefault();
                                    MethodInfo? DynamicResolvePayload = Parameter != null ? typeof(RequestSurgeon).GetMethod("ResolvePayload")?.MakeGenericMethod(Parameter.ParameterType) : null;
                                    object? ResultOfDynamicallyResolvedPayload = DynamicResolvePayload?.Invoke(null, new object[] { Context });
                                    return new RequestWrapper(Method, EndpointContainer, ResultOfDynamicallyResolvedPayload);
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
