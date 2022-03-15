using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;

namespace izolabella.Kacena.REST.Classes.Interfaces.Entities.Errors
{
    public interface IHTTPResponseError
    {
        HTTPResponseCode Code { get; }
        string Message { get; }
    }
}
