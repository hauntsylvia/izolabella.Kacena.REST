using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Arguments
{
    public class APIFormUrlEncodedCall
    {
        public U? GetCaller<U>() where U : class
        {
            return this.caller as U;
        }


        private readonly object? _caller;
        internal object? caller => _caller;


        private readonly Dictionary<string, string> _query;
        public Dictionary<string, string> query => _query;


        public APIFormUrlEncodedCall(object? caller, Dictionary<string, string> query)
        {
            this._caller = caller;
            this._query = query;
        }
    }
}
