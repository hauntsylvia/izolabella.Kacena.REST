using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Interfaces.Entities;

namespace kacena.Classes.Entities.Returns
{
    public class HTTPArrayResult<T> where T : IEntity
    {
        public HTTPArrayResult(HTTPResponseCode Code, params T[]? Result)
        {
            this.Code = Code;
            this.Results = Result;
        }
        public HTTPResponseCode Code { get; }
        public T[]? Results { get; }
    }
}
