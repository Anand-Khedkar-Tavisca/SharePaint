using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharePaint.Data.Models;
using SharePaint.Services.ServiceProviders;

namespace SharePaint.Services.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public readonly IUserServies _userService;

        public UserController(IUserServies userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            Guid userId = CurrentUserInfo.Id;
            return new JsonResult( _userService.Get(userId),_serializer);
        }

        [HttpPut("Paint/{paintId}")]
        public ActionResult Get(Guid paintId)
        {
            Guid userId = CurrentUserInfo.Id;
            return new JsonResult(_userService.AddToPaint(userId, paintId),_serializer);
        }
    }
}
