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
    public class NotFound : HTTPResponseError
    {
        public NotFound(string message = "No resource exists at this location.") : base(HTTPResponseCode.notFound, message)
        {
        }
    }
}
