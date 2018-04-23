using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharePaint.Services.Exceptions
{
    public class ExceptionBase : Exception
    {
        public HttpStatusCode _httpCode = HttpStatusCode.BadRequest;
        public ExceptionBase() : base()
        {
        }
        public ExceptionBase(String message, HttpStatusCode httpCode) : base(message)
        {
            _httpCode = httpCode;
        }
    }
}
