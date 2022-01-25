using Kacena.Classes.Bases;
using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Entities.Errors
{
    public class VerbNotAllowed : HTTPResponseError
    {
        public VerbNotAllowed(string message = "The method/verb used in this request is invalid for the requested resource.") : base(HTTPResponseCode.InternalError, message)
        {
        }
    }
}
