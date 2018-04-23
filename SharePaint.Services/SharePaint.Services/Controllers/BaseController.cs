using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SharePaint.Data.Models;

namespace SharePaint.Services.Controllers
{
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        protected JsonSerializerSettings _serializer;
        public UserPublicInfo CurrentUserInfo
        {
            get
            {
                if (HttpContext == null)
                {
                    var auth = new UserPublicInfo();
                    return auth;
                }
                object authVal;
                HttpContext.Items.TryGetValue("Authorization", out authVal);
                return authVal as UserPublicInfo;
            }
        }
        public BaseController()
        {
            // _userInfoCache = userInfoCache;
            if (_serializer == null)
            {
                _serializer = new JsonSerializerSettings();
                _serializer.Converters.Add(new StringEnumConverter());

                _serializer.NullValueHandling = NullValueHandling.Ignore;
                // _serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            }
        }
    }
}