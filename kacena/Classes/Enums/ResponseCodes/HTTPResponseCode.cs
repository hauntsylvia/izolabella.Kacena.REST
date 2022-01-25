﻿namespace kacena.Classes.Enums.ResponseCodes
{
    public enum HTTPResponseCode
    {
        RequestFulfilled = 200,
        Created = 201,
        Accepted = 202,
        NoResponse = 203,

        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,

        InternalError = 500,
        NotImplemented = 501,
        TemporarilyOverloaded = 502,

        PermanentlyMoved = 301,
    }
}
