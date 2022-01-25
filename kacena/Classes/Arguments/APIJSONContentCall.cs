using kacena.Classes.Interfaces.Entities;

namespace kacena.Classes.Arguments
{
    public class APIJSONContentCall<T> : APIBaseCall where T : IEntity
    {
        private readonly T _sentEntity;
        public T SentEntity => this._sentEntity;


        public APIJSONContentCall(object? caller, T sentEntity) : base(caller)
        {
            this._sentEntity = sentEntity;
        }
    }
}
