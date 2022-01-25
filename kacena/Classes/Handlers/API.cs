using Kacena.Classes.Arguments;
using Kacena.Classes.Attributes.HTTP;
using Kacena.Classes.Entities.Errors;
using Kacena.Classes.Entities.Returns;
using Kacena.Classes.Interfaces.Attributes.HTTP;
using Kacena.Classes.Interfaces.Bases;
using Kacena.Classes.Interfaces.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Web;

namespace Kacena.Classes.Handlers
{
    public class API
    {
        // add url reservation to not have to run as admin:
        // netsh http add urlacl url="http://*:21621/" user=everyone
        // netsh http add urlacl url="https://*:443/" user=everyone
        //server.Prefixes.Add("http://*:21621/");
        private Func<HttpListenerRequest, dynamic?>? authorizeAPICaller;
        public Func<HttpListenerRequest, dynamic?>? AuthorizeAPICaller { get => this.authorizeAPICaller; set => this.authorizeAPICaller = value; }


        private FileInfo favicon;
        public FileInfo Favicon { get => this.favicon; set => this.favicon = value == null ? throw new ArgumentNullException(nameof(value)) : (value.Exists ? value : throw new FileNotFoundException(nameof(value))); }


        private readonly Uri uri;
        public Uri Uri => this.uri;


        private readonly IController[] controllers;
        public IController[] Controllers => this.controllers;


        private readonly HttpListener listener;
        public HttpListener Listener => this.listener;


        public API(HttpListener listener, FileInfo favicon)
        {
            Type[] ts = Assembly.GetCallingAssembly().GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IController))).ToArray();
            this.controllers = new IController[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                Route? controllerRoute = (Route?)ts[i].GetCustomAttribute(typeof(Route));
                if (controllerRoute != null)
                {
                    object? controller = Activator.CreateInstance(type: ts[i], args: new object[] { this, controllerRoute.RelativeUrl.Split('/')[1] }, bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, binder: null, culture: null);
                    if (controller != null)
                        this.controllers[i] = (IController)controller;
                }
            }
            this.uri = new(listener.Prefixes.First());
            this.listener = listener;
            this.favicon = favicon;
        }


        private static bool MethodIsResourceIdentificationHandler(MethodInfo? m)
        {
            return m != null && m.GetCustomAttribute(typeof(ResourceIdentificationHandler), false) as ResourceIdentificationHandler != null;
        }


        private IController? FindRequestedController(HttpListenerContext context)
        {
            if (context.Request.Url != null)
            {
                string[] segments = context.Request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string service = segments[0];
                IController[] controllers = this.Controllers.Where(c => c.ServiceName.ToUpper() == service.ToUpper()).ToArray();
                return controllers.Length > 0 ? controllers.First() : null;
            }
            else
                return null;
        }

        private MethodInfo? FindRequestedMethod(HttpListenerContext context, IController controller)
        {
            if (context.Request.Url != null)
            {
                string[] segments = context.Request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string routeTo = segments[1];
                MethodInfo? rIdHandlerMethod = null;
                foreach (MethodInfo method in controller.GetType().GetMethods(bindingAttr: BindingFlags.Public | BindingFlags.Instance))
                {
                    Uri? methodUri = null;
                    if (MethodIsResourceIdentificationHandler(method))
                        rIdHandlerMethod = method;
                    methodUri = method.GetCustomAttribute(typeof(Route), false) is Route routingUrlAttr ? new Uri(this.Uri, $"{controller.ServiceName}{routingUrlAttr.RelativeUrl}") : null;
                    object[] customAttributes = method.GetCustomAttributes(typeof(IHTTPAttribute), false);
                    IHTTPAttribute[] methodVerbs = new IHTTPAttribute[customAttributes.Length];
                    for (int i = 0; i < customAttributes.Length; i++)
                        methodVerbs[i] = (IHTTPAttribute)customAttributes[i];
                    bool verbAllowed = methodVerbs.Any(x => x.Verb == context.Request.HttpMethod.ToUpper());
                    if (verbAllowed && methodUri != null && methodUri.GetComponents(UriComponents.Path, UriFormat.UriEscaped).ToLower() == context.Request.Url.GetComponents(UriComponents.Path, UriFormat.UriEscaped).ToLower())
                        return method;
                }
                return rIdHandlerMethod;
            }
            return null;
        }


        private async Task<ContextRequest> ProcessRequestAsync(HttpListenerContext context)
        {
            if (context.Request.Url != null)
            {
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.AddHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS, DELETE");
                context.Response.AddHeader("Access-Control-Allow-Headers", "authorization");
                string[] segments = context.Request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length > 1)
                {
                    if (this.Controllers.Length > 0)
                    {
                        IController? controller = this.FindRequestedController(context);
                        if (controller != null)
                        {
                            MethodInfo? controllerMethod = this.FindRequestedMethod(context, controller);
                            if (controllerMethod != null)
                            {
                                bool isRecId = MethodIsResourceIdentificationHandler(controllerMethod);
                                dynamic? caller = this.AuthorizeAPICaller?.Invoke(context.Request);
                                object? invokeParam;
                                if (context.Request.HttpMethod.ToUpper() == "POST" || context.Request.HttpMethod.ToUpper() == "PUT" || (context.Request.HttpMethod.ToUpper() == "PATCH" && controllerMethod.GetParameters().First().ParameterType != typeof(APIFormUrlEncodedCall) && controllerMethod.GetParameters().First().ParameterType != typeof(APIResourceIdentificationCall))) // post req
                                {
                                    object? sentEntity = null;
                                    Type controllerMethodExpectsThis = controllerMethod.GetParameters().First().ParameterType; // jsoncontcall<zenouser>
                                    Type controllerMethodExpectsThisGeneric = controllerMethodExpectsThis.GetGenericArguments().First(); // zenouser
                                    using (StreamReader clientsRequestReader = new(context.Request.InputStream, context.Request.ContentEncoding))
                                    {
                                        string clientsContent = await clientsRequestReader.ReadToEndAsync();
                                        try
                                        {
                                            JObject? preSentEntity = JsonConvert.DeserializeObject<JObject>(clientsContent);
                                            if (preSentEntity != null)
                                            {
                                                object? entity = preSentEntity.ToObject(controllerMethodExpectsThisGeneric);
                                                if (entity != null)
                                                    sentEntity = entity;
                                            }
                                        }
                                        catch (Exception ex) { Console.WriteLine(ex); }
                                    }
                                    if (sentEntity != null)
                                    {
                                        object? param = Activator.CreateInstance(controllerMethodExpectsThis, new object?[] { caller, sentEntity });
                                        if (param != null)
                                            invokeParam = param;
                                        else
                                            return new(new InternalError(), SingleResult: null);
                                    }
                                    else  // client must have an entity in a post request
                                        return new(new MalformedClientData(), SingleResult: null);
                                }
                                else // form url encoded content is expected (or no content)
                                {
                                    NameValueCollection query = HttpUtility.ParseQueryString(context.Request.Url.Query, context.Request.ContentEncoding);
                                    Dictionary<string, string> formUrlEncodedContent = new();
                                    invokeParam = new APIFormUrlEncodedCall(caller, formUrlEncodedContent);
                                    for (int i = 0; i < query.Count; i++)
                                    {
                                        string? key = query.GetKey(i);
                                        string? val = query[query.GetKey(i)];
                                        if (key != null && val != null)
                                            formUrlEncodedContent.Add(key, val);
                                    }
                                }
                                _ = ulong.TryParse(context.Request.Url.Segments.Last(), out ulong ifRecIdReq);
                                object? resultOfRequest = controllerMethod.Invoke(controller, isRecId ? new[] { invokeParam, new APIResourceIdentificationCall(caller, ifRecIdReq) } : new[] { invokeParam });
                                if (resultOfRequest != null)
                                {
                                    if (resultOfRequest is HTTPArrayResult<IEntity> finalArrayResult)
                                        return new(null, new HTTPArrayResult<IEntity>(finalArrayResult.Code, finalArrayResult.Results));
                                    else if (resultOfRequest is HTTPSingleResult<IEntity> finalSingleResult)
                                        return new(null, new HTTPSingleResult<IEntity>(finalSingleResult.Code, finalSingleResult.Result));
                                    else
                                        return new(new InternalError(), SingleResult: null);
                                }
                                else
                                    return new(new InternalError(), SingleResult: null);
                            }
                            else
                                return new(new NotFound(), SingleResult: null);
                        }
                        else
                            return new(new NotFound(), SingleResult: null);
                    }
                    else
                        return new(new NotFound(), SingleResult: null);

                }
                else if (segments.Length > 0 && segments[0] == "favicon.ico")
                {
                    ContextRequest req = new(null, SingleResult: null);
                    req.BytesToWrite = File.ReadAllBytes(this.Favicon.FullName);
                    req.WriteAsBytes = true;
                    return req;
                }
                else
                    return new(new NotFound(), SingleResult: null);
            }
            else
                return new(new InternalError(), SingleResult: null);
        }


        public async void Listen()
        {
            this.listener.IgnoreWriteExceptions = true;
            this.listener.Start();
            while (true)
                try
                {
                    HttpListenerContext Context = await this.listener.GetContextAsync();
                    APIWriter APIWriter = new(Context);
                    this.AuthorizeAPICaller?.Invoke(Context.Request);
                    ContextRequest Request = await this.ProcessRequestAsync(Context);
                    if (!Request.WriteAsBytes && Request.Success && (Request.ArrayResult != null || Request.SingleResult != null))
                    {
                        if((Request.SingleResult != null && !((int)Request.SingleResult.Code).ToString().StartsWith("2"))  ||  (Request.ArrayResult != null && !((int)Request.ArrayResult.Code).ToString().StartsWith("2")))
                        {
                            if(Request.ArrayResult != null && Request.ArrayResult.Results != null)
                                APIWriter.Write(new(Request.ArrayResult.Code, Request.ArrayResult.Results.First()));
                            else if(Request.SingleResult != null && Request.SingleResult.Result != null)
                                APIWriter.Write(new(Request.SingleResult.Code, Request.SingleResult.Result));
                        }
                        else if(Request.ArrayResult != null && Request.ArrayResult.Results != null)
                            APIWriter.Write(new(Request.ArrayResult.Code, Request.ArrayResult.Results));
                        else if (Request.SingleResult != null && Request.SingleResult.Result != null)
                            APIWriter.Write(new(Request.SingleResult.Code, Request.SingleResult.Result));
                    }
                    else if (Request.BytesToWrite != null)
                        APIWriter.WriteRawBytes(Request.BytesToWrite);
                    else if (!Request.Success && Request.Error != null)
                        APIWriter.Write(new(Request.Error.Code, Request.Error));
                    else
                        APIWriter.Write(new(Enums.ResponseCodes.HTTPResponseCode.InternalError, null));
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex);
                }
        }
    }
}
