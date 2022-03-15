using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Entities.Errors
{
    public class NotFound : HTTPResponseError
    {
        public NotFound(string message = "No resource exists at this location.") : base(HTTPResponseCode.NotFound, message)
        {
        }
    }
}
