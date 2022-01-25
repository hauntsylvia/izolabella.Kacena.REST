using KacenaRest.Classes.Enums.ResponseCodes;

namespace KacenaRest.Classes.Interfaces.Entities.Errors
{
    public interface IHTTPResponseError
    {
        HTTPResponseCode Code { get; }
        string Message { get; }
    }
}
