using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Entities.Errors
{
    public class NotFound : HTTPResponseError
    {
        public NotFound(string message = "No resource exists at this location.") : base(HTTPResponseCode.NotFound, message)
        {
        }
    }
}
