using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Arguments
{
    public class APIJSONContentCall<T> : APIBaseCall where T : IEntity
    {
        private readonly T _sentEntity;
        public T sentEntity => _sentEntity;


        public APIJSONContentCall(object? caller, T sentEntity) : base(caller)
        {
            this._sentEntity = sentEntity;
        }
    }
}
