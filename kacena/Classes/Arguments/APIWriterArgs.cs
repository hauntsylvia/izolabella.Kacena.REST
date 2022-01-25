using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Arguments
{
    public class APIWriterArgs
    {
        private readonly HTTPResponseCode code;
        public HTTPResponseCode Code => this.code;


        private readonly object? payload;
        public object Payload => this.payload ?? string.Empty;


        public APIWriterArgs(HTTPResponseCode code, object? payload)
        {
            this.code = code;
            this.payload = payload;
        }
    }
}
