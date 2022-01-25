using KacenaRest.Classes.Bases;
using KacenaRest.Classes.Enums.ResponseCodes;

namespace KacenaRest.Classes.Entities.Errors
{
    public class InternalError : HTTPResponseError
    {
        public InternalError(string message = "The server encountered an error while trying to process your request.") : base(HTTPResponseCode.InternalError, message)
        {
        }
    }
}
