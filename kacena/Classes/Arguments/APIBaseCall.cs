using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Arguments
{
    public class APIBaseCall
    {
        public U? GetCaller<U>() where U : class
        {
            return this.caller as U;
        }


        private readonly object? _caller;
        public object? caller => _caller;


        public APIBaseCall(object? caller)
        {
            this._caller = caller;
        }
    }
}
