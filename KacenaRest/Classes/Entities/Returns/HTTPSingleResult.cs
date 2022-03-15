using izolabella.Kacena.REST.Classes.Enums.ResponseCodes;
using izolabella.Kacena.REST.Classes.Interfaces.Entities;

namespace izolabella.Kacena.REST.Classes.Entities.Returns
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
