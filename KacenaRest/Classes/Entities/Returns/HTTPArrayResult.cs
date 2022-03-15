using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;
using izolabella.Kacena.REST.Classes.Interfaces.Entities;

namespace izolabella.Kacena.REST.Classes.Entities.Returns
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
