using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Entities.Errors
{
    public class NotImplemented : HTTPResponseError
    {
        public NotImplemented(string message = "This service is being worked on.") : base(HTTPResponseCode.NotImplemented, message)
        {
        }
    }
}
