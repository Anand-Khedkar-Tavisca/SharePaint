using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using SharePaint.Data.Models;
using SharePaint.Services.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharePaint.Services.Filters
{
    public class KeyAuthorizeFilter : AuthorizeFilter
    {
        IAuthonticationService _authorizationService;
        public KeyAuthorizeFilter(AuthorizationPolicy policy, IAuthonticationService authorizationService) : base(policy)
        {
            _authorizationService = authorizationService;
        }
        public override Task OnAuthorizationAsync(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Secret"))
            {
                var token = authHeader.Substring("Secret ".Length).Trim();
                UserPublicInfo userInfo;
                if (_authorizationService.IsAuthonticate(token, out userInfo))
                {
                    context.HttpContext.Items.Add("Authorization", userInfo);
                    return Task.CompletedTask;

                }
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new { error = "Unauthorized." });
                    return Task.CompletedTask;
                }
            }
            return base.OnAuthorizationAsync(context);
        }
    }
}
