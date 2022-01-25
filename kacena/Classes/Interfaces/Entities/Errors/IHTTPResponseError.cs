using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Interfaces.Entities.Errors
{
    public interface IHTTPResponseError
    {
        HTTPResponseCode Code { get; }
        string Message { get; }
    }
}
