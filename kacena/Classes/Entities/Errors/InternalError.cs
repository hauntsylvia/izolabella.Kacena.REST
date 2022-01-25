using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Entities.Errors
{
    public class InternalError : HTTPResponseError
    {
        public InternalError(string message = "The server encountered an error while trying to process your request.") : base(HTTPResponseCode.internalError, message)
        {
        }
    }
}
