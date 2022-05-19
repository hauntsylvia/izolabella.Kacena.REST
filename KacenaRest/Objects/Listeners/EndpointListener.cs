using izolabella.Kacena.REST.Objects.ErrorMessages.Base;
using izolabella.Kacena.REST.Objects.Structures;
using izolabella.Kacena.REST.Objects.Structures.Requests;
using izolabella.Kacena.REST.Objects.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Objects.Listeners
{
    public class EndpointListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Prefix">https://example.com:443/</param>
        public EndpointListener(Uri Prefix)
        {
            this.HttpListener = new();
            this.HttpListener.Prefixes.Add(Prefix.ToString());
            this.HttpListener.IgnoreWriteExceptions = true;
            this.Endpoints = new();
            this.Prefix = Prefix;
        }

        private List<IEndpointContainer> Endpoints { get; }

        public HttpListener HttpListener { get; }

        public Uri Prefix { get; }

        public void AddEndpoint(IEndpointContainer Endpoint)
        {
            this.Endpoints.Add(Endpoint);
        }

        public async Task StartListeningAsync()
        {
            this.HttpListener.Start();
            while (true)
            {
                HttpListenerContext Context = await this.HttpListener.GetContextAsync();
                RequestWrapper? SurgeonResult = RequestSurgeon.CutRequest(Context, this.Endpoints.ToArray());
                if (SurgeonResult != null)
                {
                    object? Result = await SurgeonResult.Invoke();
                    if (Result != null && Context.Response.OutputStream.CanWrite)
                    {
                        if(Result is ErrorMessage EM)
                        {
                            Context.Response.StatusCode = (int)EM.Code;   
                        }
                        using StreamWriter StreamWriter = new(Context.Response.OutputStream);
                        StreamWriter.Write(JsonConvert.SerializeObject(Result));
                    }
                }
                Context.Response.OutputStream.Dispose();
            }
        }

        public Task StopListening()
        {
            this.HttpListener.Stop();
            return Task.CompletedTask;
        }
    }
}
