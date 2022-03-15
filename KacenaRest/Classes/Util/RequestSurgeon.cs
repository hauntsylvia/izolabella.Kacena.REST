using izolabella.Kacena.REST.Classes.Structures;
using izolabella.Kacena.REST.Classes.Structures.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace izolabella.Kacena.REST.Classes.Util
{
    internal class RequestSurgeon
    {
        private static T? ResolvePayload<T>(HttpListenerContext Context)
        {
            T? Payload = default;
            if (Context.Request.ContentType != null && Context.Request.InputStream != null && Context.Request.InputStream.CanRead)
            {
                using(StreamReader ContextReader = new(Context.Request.InputStream))
                {
                    if (Context.Request.ContentType == "application/json")
                    {
                        Payload = JsonConvert.DeserializeObject<T>(ContextReader.ReadToEnd());
                    }
                    else if(Context.Request.ContentType == "application/x-www-form-urlencoded")
                    {
                        _ = new QueryStringConverter<T>(Context.Request.QueryString).TryConvert(out Payload);
                    }
                }
            }
            return Payload;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type of object that is expected within requests such as POST requests.</typeparam>
        /// <param name="Context"></param>
        /// <returns></returns>
        internal static RequestWrapper<T>? CutRequest<T>(HttpListenerContext Context, Uri Prefix, params IEndpoint[] Endpoints)
        {
            if (Context.Request.Url != null)
            {
                string[] Parts = Context.Request.Url.AbsolutePath.Split('/');
                string RequestedEndpoint = Parts[1];
                string? RequestedMethod = Parts.Length > 1 ? Parts[2] : null;
                if(RequestedMethod != null)
                {
                    foreach (IEndpoint Endpoint in Endpoints)
                    {
                        if (RequestedEndpoint.ToLower() == Endpoint.Route.ToLower().Trim('/'))
                        {
                            foreach (MethodInfo Method in Endpoint.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                            {
                                if(Method.Name.ToLower() == RequestedMethod.ToLower())
                                {
                                    return new RequestWrapper<T>(Method, ResolvePayload<T>(Context));
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
