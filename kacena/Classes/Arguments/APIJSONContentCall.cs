using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Arguments
{
    public class APIJSONContentCall<T> where T : IEntity
    {
        public U? GetCaller<U>() where U : class
        {
            return this.caller as U;
        }


        private readonly object? _caller;
        internal object? caller => _caller;


        private readonly T _sentEntity;
        public T sentEntity => _sentEntity;


        public APIJSONContentCall(object? caller, T sentEntity)
        {
            this._caller = caller;
            this._sentEntity = sentEntity;
        }
    }
}
