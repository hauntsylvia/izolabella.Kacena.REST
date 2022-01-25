using kacena.Classes.Arguments;
using kacena.Classes.Interfaces.Entities.Errors;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace kacena.Classes.Handlers
{
    public class APIWriter
    {
        private readonly HttpListenerContext _context;
        public HttpListenerContext context => this._context;

        public APIWriter(HttpListenerContext context)
        {
            this._context = context;
        }
        public async void WriteRawBytes(byte[] bitebite)
        {
            this.context.Response.StatusCode = this.context.Response.StatusCode == default ? 200 : this.context.Response.StatusCode;
            if (this.context.Response.OutputStream.CanWrite)
                await this.context.Response.OutputStream.WriteAsync(bitebite);
            this.context.Response.Close();
        }


        public void WriteRaw(string write)
        {
            byte[] bitebitebite = Encoding.UTF8.GetBytes(write);
            this.context.Response.KeepAlive = false;
            this.WriteRawBytes(bitebitebite);
        }


        public void Write(APIWriterArgs write)
        {
            this.context.Response.StatusCode = (int)write.code;
            this.WriteRaw(JsonConvert.SerializeObject(write.payload, Formatting.None));
        }


        public void Throw(IHTTPResponseError error)
        {
            this.context.Response.StatusCode = (int)error.code;
            this.WriteRaw(JsonConvert.SerializeObject(error, Formatting.Indented));
            throw new Exception(error.message);
        }
    }
}
