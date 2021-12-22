using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kacena.Classes.Interfaces.Entities.Errors;
using kacena.Classes.Enums.ResponseCodes;
using kacena.Classes.Bases;

namespace kacena.Classes.Entities.Error
{
    public class VerbNotAllowed : HTTPResponseError
    {
        public VerbNotAllowed(string message = "The method/verb used in this request is invalid for the requested resource.") : base(HTTPResponseCode.internalError, message)
        {
        }
    }
}
