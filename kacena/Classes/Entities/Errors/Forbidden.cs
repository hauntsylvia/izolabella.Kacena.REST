using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Entities.Errors
{
    public class Forbidden : HTTPResponseError
    {
        public Forbidden(string message, bool clientIsAuthenticated) : base(clientIsAuthenticated ? HTTPResponseCode.Forbidden : HTTPResponseCode.Unauthorized, message)
        {
        }
    }
}
