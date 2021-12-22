using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kacena.Classes.Interfaces.Entities.Errors;
using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Bases;

namespace kacena.Classes.Entities.Errors
{
    public class Forbidden : HTTPResponseError
    {
        public Forbidden(string message, bool clientIsAuthenticated) : base(clientIsAuthenticated ? HTTPResponseCode.forbidden : HTTPResponseCode.unauthorized, message)
        {
        }
    }
}
