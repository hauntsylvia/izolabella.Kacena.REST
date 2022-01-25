using KacenaRest.Classes.Enums.ResponseCodes;
using KacenaRest.Classes.Interfaces.Entities;

namespace KacenaRest.Classes.Entities.Returns
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
