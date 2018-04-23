using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharePaint.Services.Exceptions
{
    public class UnAuthorizedException : ExceptionBase
    {
        public UnAuthorizedException() : base()
        {
        }
        public UnAuthorizedException(String message, HttpStatusCode httpCode = HttpStatusCode.Unauthorized) : base(message, httpCode)
        {
            _httpCode = httpCode;
        }
    }
}
