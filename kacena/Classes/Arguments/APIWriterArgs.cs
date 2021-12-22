using kacena.Classes.Enums.ResponseCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using kacena.Classes.Interfaces;

namespace kacena.Classes.Arguments
{
    public class APIWriterArgs
    {
        private readonly HTTPResponseCode _code;
        public HTTPResponseCode code => _code;


        private readonly object? _payload;
        public object payload => _payload ?? string.Empty;


        public APIWriterArgs(HTTPResponseCode code, object? payload)
        {
            this._code = code;
            this._payload = payload;
        }
    }
}
