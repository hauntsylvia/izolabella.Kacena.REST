using KacenaRest.Classes.Bases;
using KacenaRest.Classes.Enums.ResponseCodes;

namespace KacenaRest.Classes.Entities.Errors
{
    public class NotImplemented : HTTPResponseError
    {
        public NotImplemented(string message = "This service is being worked on.") : base(HTTPResponseCode.NotImplemented, message)
        {
        }
    }
}
