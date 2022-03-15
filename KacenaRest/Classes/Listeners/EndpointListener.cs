using izolabella.Kacena.REST.Classes.Structures;
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
        }


        private List<IEndpoint> Endpoints { get; }


        private readonly HttpListener httpListener;


        public HttpListener HttpListener => this.httpListener;


        public void AddEndpoint(IEndpoint Endpoint)
        {
            this.Endpoints.Add(Endpoint);
        }

        public async Task StartListeningAsync()
        {
            this.HttpListener.Start();
            HttpListenerContext GetContext = await this.HttpListener.GetContextAsync();
        }
    }
}
