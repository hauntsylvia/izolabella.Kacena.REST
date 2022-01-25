using Kacena.Classes.Bases;
using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Entities.Errors
{
    public class InternalError : HTTPResponseError
    {
        public InternalError(string message = "The server encountered an error while trying to process your request.") : base(HTTPResponseCode.InternalError, message)
        {
        }
    }
}
