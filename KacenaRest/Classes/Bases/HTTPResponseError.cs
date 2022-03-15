using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;
using izolabella.Kacena.REST.Classes.Interfaces.Entities.Errors;
using Newtonsoft.Json;

namespace izolabella.Kacena.REST.Classes.Bases
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class HTTPResponseError : IHTTPResponseError
    {
        [JsonConstructor]
        public HTTPResponseError(HTTPResponseCode code, string message)
        {
            this.code = code;
            this.message = message;
        }

        /// <summary>
        /// the error code returned to the requesting client
        /// </summary>
        private readonly HTTPResponseCode code;
        public HTTPResponseCode Code => this.code;


        [JsonProperty("message")]
        /// <summary>
        /// the property "message" of the json payload if this is returned to the context writer
        /// </summary>
        private readonly string message;
        public string Message => this.message;
    }
}
