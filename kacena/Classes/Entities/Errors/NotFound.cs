using Kacena.Classes.Bases;
using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Entities.Errors
{
    public class NotFound : HTTPResponseError
    {
        public NotFound(string message = "No resource exists at this location.") : base(HTTPResponseCode.NotFound, message)
        {
        }
    }
}
