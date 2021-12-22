using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Entities.Returns
{
    public class HTTPResult<T> where T : IEntity
    {
        public HTTPResult(HTTPResponseCode code, params T[]? result)
        {
            this.code = code;
            this.results = result;
        }
        public HTTPResponseCode code { get; }
        public T[]? results { get; }
    }
}
