using KacenaRest.Classes.Bases;
using KacenaRest.Classes.Enums.ResponseCodes;

namespace KacenaRest.Classes.Entities.Errors
{
    public class Forbidden : HTTPResponseError
    {
        public Forbidden(string message, bool clientIsAuthenticated) : base(clientIsAuthenticated ? HTTPResponseCode.Forbidden : HTTPResponseCode.Unauthorized, message)
        {
        }
    }
}
