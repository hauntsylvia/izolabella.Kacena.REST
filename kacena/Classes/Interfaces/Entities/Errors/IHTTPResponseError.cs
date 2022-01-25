using kacena.Classes.Enums.ResponseCodes;

namespace kacena.Classes.Interfaces.Entities.Errors
{
    public interface IHTTPResponseError
    {
        HTTPResponseCode code { get; }
        string message { get; }
    }
}
