using Kacena.Classes.Bases;
using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Entities.Errors
{
    public class Forbidden : HTTPResponseError
    {
        public Forbidden(string message, bool clientIsAuthenticated) : base(clientIsAuthenticated ? HTTPResponseCode.Forbidden : HTTPResponseCode.Unauthorized, message)
        {
        }
    }
}
