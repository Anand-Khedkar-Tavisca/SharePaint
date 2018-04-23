using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharePaint.Data.Models;
using SharePaint.Data.Services;
using SharePaint.Services.Exceptions;

namespace SharePaint.Services.ServiceProviders
{
    public class PaintServiceProvider : IPaintServies
    {
        private readonly IPaintDataService _paintDataService;
        private readonly IUserDataService _userDataService;
        private readonly IStrokeDataService _strokeDataService;
        private readonly ILineDataService _lineDataService;

        public PaintServiceProvider(IPaintDataService paintDataService, IUserDataService userDataService, IStrokeDataService strokeDataService, ILineDataService lineDataService)
        {
            _paintDataService = paintDataService;
            _userDataService = userDataService;
            _strokeDataService = strokeDataService;
           _lineDataService = lineDataService;
        }
        public Paint CreatePaint(Paint paint)
        {
           return _paintDataService.Create(paint);
        }

        public Paint CreatePaint()
        {
            return _paintDataService.Create(new Paint());
        }

        public Paint CreateOrGetPaint(Guid userId)
        {
           var painUser =  _userDataService.Get(userId);
            if (painUser.PaintId != Guid.Empty)
            {
                var paint = _paintDataService.Get(painUser.PaintId);
                if(paint==null)
                {
                    throw new NotFoundException("Paint Not found.");
                }
                paint.Strokes = _strokeDataService.GetByPaintId(paint.Id);
                foreach (var stroke in paint.Strokes)
                {
                    stroke.Lines = _lineDataService.GetByStrockId(stroke.Id);
                }
                return paint;
            }
            else
            {
               var paint = _paintDataService.Create(new Paint());
                painUser.PaintId = paint.Id;
                return paint;
            }
        }
    }
}
