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
    public class MalformedClientData : HTTPResponseError
    {
        public MalformedClientData(string message = "Malformed client data.") : base(HTTPResponseCode.badRequest, message)
        {
        }
    }
}
