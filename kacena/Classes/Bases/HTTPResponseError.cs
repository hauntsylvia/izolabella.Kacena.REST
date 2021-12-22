using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Interfaces.Entities.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Bases
{
    public class HTTPResponseError : IHTTPResponseError
    {
        public HTTPResponseError(HTTPResponseCode code, string message)
        {
            this._code = code;
            this._message = message;
        }

        /// <summary>
        /// the error code returned to the requesting client
        /// </summary>
        private readonly HTTPResponseCode _code;
        public HTTPResponseCode code => _code;


        /// <summary>
        /// the property "message" of the json payload if this is returned to the context writer
        /// </summary>
        private readonly string _message;
        public string message => _message;
    }
}
