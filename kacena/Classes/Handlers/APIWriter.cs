using kacena.Classes.Arguments;
using kacena.Classes.Interfaces.Entities.Errors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Handlers
{
    public class APIWriter
    {
        private readonly HttpListenerContext _context;
        public HttpListenerContext context => _context;

        public APIWriter(HttpListenerContext context)
        {
            this._context = context;
        }
        public async void WriteRawBytes(byte[] bitebite)
        {
            if (this.context.Response.OutputStream.CanWrite)
                await this.context.Response.OutputStream.WriteAsync(bitebite);
            this.context.Response.Close();
        }


        public void WriteRaw(string write)
        {
            byte[] bitebitebite = Encoding.UTF8.GetBytes(write);
            this.context.Response.KeepAlive = false;
            WriteRawBytes(bitebitebite);
        }


        public void Write(APIWriterArgs write)
        {
            this.context.Response.StatusCode = (int)write.code;
            WriteRaw(JsonConvert.SerializeObject(write.payload, Formatting.None));
        }


        public void Throw(IHTTPResponseError error)
        {
            this.context.Response.StatusCode = (int)error.code;
            WriteRaw(JsonConvert.SerializeObject(error, Formatting.Indented));
            throw new Exception(error.message);
        }
    }
}
