using KacenaRest.Classes.Bases;
using KacenaRest.Classes.Enums.ResponseCodes;

namespace KacenaRest.Classes.Entities.Errors
{
    public class MalformedClientData : HTTPResponseError
    {
        public MalformedClientData(string message = "Malformed client data.") : base(HTTPResponseCode.BadRequest, message)
        {
        }
    }
}
