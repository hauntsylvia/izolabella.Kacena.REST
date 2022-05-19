using izolabella.Kacena.REST.Objects.ErrorMessages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Kacena.REST.Objects.Constants
{
    public static class Errors
    {
        public static ErrorMessage MissingPayload => new(HttpStatusCode.BadRequest, "Method requires missing object.", "Please add an object to send.");
    }
}
