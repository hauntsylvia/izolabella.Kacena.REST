using kacena.Classes.Enums.ResponseCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Interfaces.Entities.Errors
{
    public interface IHTTPResponseError
    {
        HTTPResponseCode code { get; }
        string message { get; }
    }
}
