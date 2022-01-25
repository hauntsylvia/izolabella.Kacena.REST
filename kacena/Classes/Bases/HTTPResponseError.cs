using Kacena.Classes.Enums.ResponseCodes;
using Kacena.Classes.Interfaces.Entities.Errors;
using Newtonsoft.Json;

namespace Kacena.Classes.Bases
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class HTTPResponseError : IHTTPResponseError
    {
        [JsonConstructor]
        public HTTPResponseError(HTTPResponseCode code, string message)
        {
            this._code = code;
            this._message = message;
        }

        /// <summary>
        /// the error code returned to the requesting client
        /// </summary>
        private readonly HTTPResponseCode _code;
        public HTTPResponseCode Code => this._code;


        [JsonProperty("message")]
        /// <summary>
        /// the property "message" of the json payload if this is returned to the context writer
        /// </summary>
        private readonly string _message;
        public string Message => this._message;
    }
}
