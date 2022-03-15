using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Entities.Errors
{
    public class NotImplemented : HTTPResponseError
    {
        public NotImplemented(string message = "This service is being worked on.") : base(HTTPResponseCode.NotImplemented, message)
        {
        }
    }
}
