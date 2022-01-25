using Kacena.Classes.Enums.ResponseCodes;

namespace Kacena.Classes.Interfaces.Entities.Errors
{
    public interface IHTTPResponseError
    {
        HTTPResponseCode Code { get; }
        string Message { get; }
    }
}
