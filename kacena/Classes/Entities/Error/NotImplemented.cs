using kacena.Classes.Bases;
using kacena.Classes.Enums.ResponseCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kacena.Classes.Interfaces.Entities.Errors;

namespace kacena.Classes.Entities.Error
{
    public class NotImplemented : HTTPResponseError
    {
        public NotImplemented(string message = "This service is being worked on.") : base(HTTPResponseCode.notImplemented, message)
        {
        }
    }
}
