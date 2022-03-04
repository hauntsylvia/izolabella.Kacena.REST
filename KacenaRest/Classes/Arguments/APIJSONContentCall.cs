using KacenaRest.Classes.Interfaces.Entities;

namespace KacenaRest.Classes.Arguments
{
    public class APIJSONContentCall<T> : APIBaseCall where T : IEntity
    {
        private readonly T sentEntity;
        public T SentEntity => this.sentEntity;


        public APIJSONContentCall(object? caller, T sentEntity) : base(caller)
        {
            this.sentEntity = sentEntity;
        }
    }
}
