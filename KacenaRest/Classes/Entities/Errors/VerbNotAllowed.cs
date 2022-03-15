using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Entities.Errors
{
    public class VerbNotAllowed : HTTPResponseError
    {
        public VerbNotAllowed(string message = "The method/verb used in this request is invalid for the requested resource.") : base(HTTPResponseCode.InternalError, message)
        {
        }
    }
}
