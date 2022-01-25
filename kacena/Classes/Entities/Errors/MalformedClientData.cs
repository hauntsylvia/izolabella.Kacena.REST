using Kacena.Classes.Bases;
using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Entities.Errors
{
    public class MalformedClientData : HTTPResponseError
    {
        public MalformedClientData(string message = "Malformed client data.") : base(HTTPResponseCode.BadRequest, message)
        {
        }
    }
}
