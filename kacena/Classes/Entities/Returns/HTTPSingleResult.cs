using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Interfaces.Entities;

namespace kacena.Classes.Entities.Returns
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
