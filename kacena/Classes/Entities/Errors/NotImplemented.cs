using Kacena.Classes.Bases;
using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Entities.Errors
{
    public class NotImplemented : HTTPResponseError
    {
        public NotImplemented(string message = "This service is being worked on.") : base(HTTPResponseCode.NotImplemented, message)
        {
        }
    }
}
