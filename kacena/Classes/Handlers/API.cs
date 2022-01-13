using kacena.Classes.Attributes.HTTP;
using kacena.Classes.Entities.Returns;
using kacena.Classes.Interfaces.Attributes.HTTP;
using kacena.Classes.Interfaces.Bases;
using kacena.Classes.Interfaces.Entities;
using System.Net;
using System.Reflection;
using kacena.Classes.Entities.Errors;
using kacena.Classes.Arguments;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Text;

namespace kacena.Classes.Handlers
{
    public class API
    {
        // add url reservation to not have to run as admin:
        // netsh http add urlacl url="http://*:21621/" user=everyone
        // netsh http add urlacl url="https://*:443/" user=everyone
        //server.Prefixes.Add("http://*:21621/");
        private Func<HttpListenerRequest, dynamic?>? _authorizeAPICaller;
        public Func<HttpListenerRequest, dynamic?>? authorizeAPICaller { get => _authorizeAPICaller; set => _authorizeAPICaller = value; }


        private FileInfo _favicon;
        public FileInfo favicon { get => _favicon; set => _favicon = value == null ? throw new ArgumentNullException(nameof(value)) : (value.Exists ? value : throw new FileNotFoundException(nameof(value))); }


        private readonly Uri _uri;
        public Uri uri => _uri;


        private readonly IController[] _controllers;
        public IController[] controllers => _controllers;


        private readonly HttpListener _listener;
        public HttpListener listener => _listener;


        public API(HttpListener listener, FileInfo favicon)
        {
            Type[] ts = Assembly.GetCallingAssembly().GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IController))).ToArray();
            this._controllers = new IController[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                Route? controllerRoute = (Route?)ts[i].GetCustomAttribute(typeof(Route));
                if(controllerRoute != null)
                {
                    object? controller = Activator.CreateInstance(type: ts[i], args: new object[] { this, controllerRoute.relativeUri.Split('/')[1] }, bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, binder: null, culture: null);
                    if (controller != null)
                        this._controllers[i] = (IController)controller;
                }
            }
            this._uri = new(listener.Prefixes.First());
            this._listener = listener;
            this._favicon = favicon;
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
                IController[] controllers = this.controllers.Where(c => c.serviceName.ToUpper() == service.ToUpper()).ToArray();
                return controllers.Length > 0 ? controllers.First() : null;
            }
            else
                return null;
        }

        private MethodInfo? FindRequestedMethod(HttpListenerContext context, IController controller)
        {
            if(context.Request.Url != null)
            {
                string[] segments = context.Request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                string routeTo = segments[1];
                MethodInfo? rIdHandlerMethod = null;
                foreach (MethodInfo method in controller.GetType().GetMethods(bindingAttr: BindingFlags.Public | BindingFlags.Instance))
                {
                    Uri? methodUri = null;
                    if (MethodIsResourceIdentificationHandler(method))
                        rIdHandlerMethod = method;
                    methodUri = method.GetCustomAttribute(typeof(Route), false) is Route routingUrlAttr ? new Uri(this.uri, $"{controller.serviceName}{routingUrlAttr.relativeUri}") : null;
                    object[] customAttributes = method.GetCustomAttributes(typeof(IHTTPAttribute), false);
                    IHTTPAttribute[] methodVerbs = new IHTTPAttribute[customAttributes.Length];
                    for (int i = 0; i < customAttributes.Length; i++)
                        methodVerbs[i] = (IHTTPAttribute)customAttributes[i];
                    bool verbAllowed = methodVerbs.Any(x => x.verb == context.Request.HttpMethod.ToUpper());
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
                    if(this.controllers.Length > 0)
                    {
                        IController? controller = this.FindRequestedController(context);
                        if(controller != null)
                        {
                            MethodInfo? controllerMethod = this.FindRequestedMethod(context, controller);
                            if (controllerMethod != null)
                            {
                                bool isRecId = MethodIsResourceIdentificationHandler(controllerMethod);
                                dynamic? caller = this.authorizeAPICaller?.Invoke(context.Request);
                                object? invokeParam;
                                if (context.Request.HttpMethod.ToUpper() == "POST" || context.Request.HttpMethod.ToUpper() == "PUT" || context.Request.HttpMethod.ToUpper() == "PATCH" && controllerMethod.GetParameters().First().ParameterType != typeof(APIFormUrlEncodedCall) && controllerMethod.GetParameters().First().ParameterType != typeof(APIResourceIdentificationCall)) // post req
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
                                            return new(new InternalError(), null);
                                    }
                                    else  // client must have an entity in a post request
                                        return new(new MalformedClientData(), null);
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
                                if (resultOfRequest != null && resultOfRequest.GetType() == typeof(HTTPResult<IEntity>))
                                {
                                    HTTPResult<IEntity> finalResult = (HTTPResult<IEntity>)resultOfRequest;
                                    return new(null, new HTTPResult<IEntity>(finalResult.code, finalResult.results));
                                }
                                else
                                    return new(new InternalError(), null);
                            }
                            else
                                return new(new NotFound(), null);
                        }
                        else
                            return new(new NotFound(), null);
                    }
                    else
                        return new(new NotFound(), null);

                }
                else if (segments.Length > 0 && segments[0] == "favicon.ico")
                {
                    ContextRequest req = new(null, null);
                    req.bytesToWrite = File.ReadAllBytes(this.favicon.FullName);
                    req.writeAsBytes = true;
                    return req;
                }
                else
                    return new(new NotFound(), null);
            }
            else
                return new(new InternalError(), null);
        }


        public async void Listen()
        {
            _listener.IgnoreWriteExceptions = true;
            _listener.Start();
            while (true)
                try
                {
                    HttpListenerContext context = await _listener.GetContextAsync();
                    APIWriter writer = new(context);
                    this.authorizeAPICaller?.Invoke(context.Request);
                    ContextRequest req = await this.ProcessRequestAsync(context);
                    if (!req.writeAsBytes && req.success && req.result != null && req.result.results != null)
                    {
                        if (req.result.results.Length > 1 || req.result.results.Length == 0)
                            writer.Write(new(req.result.code, req.result.results));
                        else if (req.result.results.Length == 1)
                            writer.Write(new(req.result.code, req.result.results.First()));
                    }
                    else if (req.bytesToWrite != null)
                        writer.WriteRawBytes(req.bytesToWrite);
                    else if (!req.success && req.error != null)
                        writer.Write(new(req.error.code, req.error));
                    else
                        writer.Write(new(Enums.ResponseCodes.HTTPResponseCode.internalError, null));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
        }
    }
}
