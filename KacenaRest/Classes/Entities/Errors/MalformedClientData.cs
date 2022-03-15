using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Entities.Errors
{
    public class MalformedClientData : HTTPResponseError
    {
        public MalformedClientData(string message = "Malformed client data.") : base(HTTPResponseCode.BadRequest, message)
        {
        }
    }
}
