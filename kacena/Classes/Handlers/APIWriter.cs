using Kacena.Classes.Arguments;
using Kacena.Classes.Interfaces.Entities.Errors;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Kacena.Classes.Handlers
{
    public class APIWriter
    {
        private readonly HttpListenerContext context;
        public HttpListenerContext Context => this.context;

        public APIWriter(HttpListenerContext context)
        {
            this.context = context;
        }
        public async void WriteRawBytes(byte[] bitebite)
        {
            this.Context.Response.StatusCode = this.Context.Response.StatusCode == default ? 200 : this.Context.Response.StatusCode;
            if (this.Context.Response.OutputStream.CanWrite)
                await this.Context.Response.OutputStream.WriteAsync(bitebite);
            this.Context.Response.Close();
        }


        public void WriteRaw(string write)
        {
            byte[] bitebitebite = Encoding.UTF8.GetBytes(write);
            this.Context.Response.KeepAlive = false;
            this.WriteRawBytes(bitebitebite);
        }


        public void Write(APIWriterArgs write)
        {
            this.Context.Response.StatusCode = (int)write.Code;
            this.WriteRaw(JsonConvert.SerializeObject(write.Payload, Formatting.None));
        }


        public void Throw(IHTTPResponseError error)
        {
            this.Context.Response.StatusCode = (int)error.Code;
            this.WriteRaw(JsonConvert.SerializeObject(error, Formatting.Indented));
            throw new Exception(error.Message);
        }
    }
}
