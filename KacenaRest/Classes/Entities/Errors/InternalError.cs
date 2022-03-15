using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Entities.Errors
{
    public class InternalError : HTTPResponseError
    {
        public InternalError(string message = "The server encountered an error while trying to process your request.") : base(HTTPResponseCode.InternalError, message)
        {
        }
    }
}
