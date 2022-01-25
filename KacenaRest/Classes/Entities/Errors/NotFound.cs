using KacenaRest.Classes.Bases;
using KacenaRest.Classes.Enums.ResponseCodes;

namespace KacenaRest.Classes.Entities.Errors
{
    public class NotFound : HTTPResponseError
    {
        public NotFound(string message = "No resource exists at this location.") : base(HTTPResponseCode.NotFound, message)
        {
        }
    }
}
