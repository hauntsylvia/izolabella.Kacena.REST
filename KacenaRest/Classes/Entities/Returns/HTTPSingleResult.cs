using KacenaRest.Classes.Enums.ResponseCodes;
using KacenaRest.Classes.Interfaces.Entities;

namespace KacenaRest.Classes.Entities.Returns
{
    public class HTTPSingleResult<T> where T : IEntity
    {
        public HTTPSingleResult(HTTPResponseCode Code, T? Result)
        {
            this.Code = Code;
            this.Result = Result;
        }
        public HTTPResponseCode Code { get; }
        public T? Result { get; }
    }
}
