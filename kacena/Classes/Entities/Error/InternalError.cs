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
    public class InternalError : HTTPResponseError
    {
        public InternalError(string message = "The server encountered an error while trying to process your request.") : base(HTTPResponseCode.internalError, message)
        {
        }
    }
}
