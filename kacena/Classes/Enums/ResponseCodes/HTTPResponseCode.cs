using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacena.Classes.Enums.ResponseCodes
{
    public enum HTTPResponseCode
    {
        requestFulfilled = 200,
        created = 201,
        accepted = 202,
        noResponse = 203,

        badRequest = 400,
        unauthorized = 401,
        forbidden = 403,
        notFound = 404,

        internalError = 500,
        notImplemented = 501,
        temporarilyOverloaded = 502,

        permanentlyMoved = 301,
    }
}
