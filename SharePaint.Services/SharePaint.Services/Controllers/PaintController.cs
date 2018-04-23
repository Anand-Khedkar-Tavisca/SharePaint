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
    public class PaintController : BaseController
    {
        public readonly IPaintServies _paintService;

        public PaintController(IPaintServies paintService)
        {
            _paintService = paintService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            Guid userId = CurrentUserInfo.Id;
            return  new JsonResult( _paintService.CreateOrGetPaint(userId), _serializer);
        }
    }
}
