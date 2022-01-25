using Kacena.Classes.Enums.ResponseCodes;
using Kacena.Classes.Interfaces.Entities;

namespace Kacena.Classes.Entities.Returns
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
