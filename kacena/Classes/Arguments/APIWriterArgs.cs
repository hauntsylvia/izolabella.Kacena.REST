using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Arguments
{
    public class APIWriterArgs
    {
        private readonly HTTPResponseCode _code;
        public HTTPResponseCode code => this._code;


        private readonly object? _payload;
        public object payload => this._payload ?? string.Empty;


        public APIWriterArgs(HTTPResponseCode code, object? payload)
        {
            this._code = code;
            this._payload = payload;
        }
    }
}
