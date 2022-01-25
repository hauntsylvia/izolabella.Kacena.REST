using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Entities.Errors
{
    public class MalformedClientData : HTTPResponseError
    {
        public MalformedClientData(string message = "Malformed client data.") : base(HTTPResponseCode.badRequest, message)
        {
        }
    }
}
