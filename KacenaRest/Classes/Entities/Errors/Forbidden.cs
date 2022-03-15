using izolabella.Kacena.REST.Classes.Bases;
using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Entities.Errors
{
    public class Forbidden : HTTPResponseError
    {
        public Forbidden(string message, bool clientIsAuthenticated) : base(clientIsAuthenticated ? HTTPResponseCode.Forbidden : HTTPResponseCode.Unauthorized, message)
        {
        }
    }
}
