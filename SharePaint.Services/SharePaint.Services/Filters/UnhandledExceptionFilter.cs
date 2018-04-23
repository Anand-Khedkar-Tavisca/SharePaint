using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharePaint.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharePaint.Services.Filters
{
    public class UnhandledExceptionFilter
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initialize middleware
        /// </summary>
        /// <param name="next"></param>
        public UnhandledExceptionFilter(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke a task with exception handling
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                // Release this exception back into the wild after tracking it down
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string message ="";
            var cException = exception as ExceptionBase;
            if (cException != null)
            {
                message = cException.Message;
                code = cException._httpCode;
            }
            else
            {
                //log exxception
                message = exception.Message;
            }

            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
