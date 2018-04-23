using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharePaint.Services.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException() : base()
        {
        }
        public NotFoundException(String message, HttpStatusCode httpCode = HttpStatusCode.NotFound) : base(message, httpCode)
        {
            _httpCode = httpCode;
        }
    }
}
