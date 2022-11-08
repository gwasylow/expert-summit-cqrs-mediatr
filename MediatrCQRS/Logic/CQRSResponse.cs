using System;
using System.Net;

namespace MediatrCQRS.Logic
{
    public abstract class CQRSResponse
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
        public string ErrorMessage { get; init; }
    }
}
