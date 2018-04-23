using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharePaint.Data.Models;
using ShareStroke.Services.ServiceProviders;

namespace SharePaint.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Stroke")]
    public class StrokeController : BaseController
    {
        private readonly IStrokeService _strokeService;
        private readonly ILineService _lineService;

        public StrokeController(IStrokeService strokeService, ILineService lineService)
        {
            _strokeService = strokeService;
            _lineService = lineService;
        }
        [HttpGet("{strokeId}")]
        public ActionResult Get(Guid strokeId)
        {
           return new JsonResult(_strokeService.Get(strokeId),_serializer);
        }

        [HttpPost]
        public ActionResult Create([FromBody]Stroke stroke)
        {
            stroke.UserId = CurrentUserInfo.Id;
            stroke.PaintId = CurrentUserInfo.PaintId;
            return new JsonResult(_strokeService.CreateStroke(stroke), _serializer);
        }

        [HttpPost("{strokeId}/Line")]
        public ActionResult CreateLine(Guid strokeId, [FromBody]Line line)
        {
            line.StrokeId = strokeId;
            return new JsonResult( _lineService.CreateLine(line),_serializer);
        }
    }
}