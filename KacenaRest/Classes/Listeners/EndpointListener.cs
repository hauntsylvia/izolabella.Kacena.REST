using izolabella.Kacena.REST.Classes.Structures;
using izolabella.Kacena.REST.Classes.Structures.Requests;
using izolabella.Kacena.REST.Classes.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Classes.Listeners
{
    public class EndpointListener
    {
        public EndpointListener(Uri Prefix)
        {
            this.httpListener = new();
            this.httpListener.Prefixes.Add(Prefix.ToString());
            this.httpListener.IgnoreWriteExceptions = true;
            this.Endpoints = new();
            this.Prefix = Prefix;
        }


        private List<IEndpointContainer> Endpoints { get; }


        private readonly HttpListener httpListener;


        public HttpListener HttpListener => this.httpListener;

        public Uri Prefix { get; }

        public void AddEndpoint(IEndpointContainer Endpoint)
        {
            this.Endpoints.Add(Endpoint);
        }

        public async Task StartListeningAsync()
        {
            this.HttpListener.Start();
            while(true)
            {
                HttpListenerContext Context = await this.HttpListener.GetContextAsync();
                RequestWrapper? SurgeonResult = RequestSurgeon.CutRequest(Context, this.Endpoints.ToArray());
                if (SurgeonResult != null)
                {
                    object? Result = await SurgeonResult.Invoke();
                    if (Result != null && Context.Response.OutputStream.CanWrite)
                    using (StreamWriter StreamWriter = new(Context.Response.OutputStream))
                        {
                            StreamWriter.Write(JsonConvert.SerializeObject(Result));
                        }
                }
                Context.Response.OutputStream.Dispose();
            }
        }
    }
}
