using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Arguments
{
    public class APIFormUrlEncodedCall : APIBaseCall
    {
        private readonly Dictionary<string, string> _query;
        public Dictionary<string, string> query => _query;


        public APIFormUrlEncodedCall(object? caller, Dictionary<string, string> query) : base(caller)
        {
            this._query = query;
        }
    }
}
