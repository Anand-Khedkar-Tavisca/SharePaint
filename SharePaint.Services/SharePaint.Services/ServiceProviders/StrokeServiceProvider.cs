using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharePaint.Data.Models;
using SharePaint.Data.Services;
using ShareStroke.Data.Services;

namespace ShareStroke.Services.ServiceProviders
{
    public class StrokeServiceProvider : IStrokeService
    {
        private readonly IStrokeDataService _strokeDataService;
        private readonly IUserDataService _userDataService;
        private readonly ILineDataService _lineDataService;

        public StrokeServiceProvider(IStrokeDataService strokeDataService, IUserDataService userDataService, ILineDataService lineDataService)
        {
            _strokeDataService = strokeDataService;
            _userDataService = userDataService;
            _lineDataService = lineDataService;
        }

        public ILineDataService LineDataService { get; }

   
        public Stroke CreateStroke(Stroke stroke)
        {
           return _strokeDataService.Create(stroke);
        }

 
        public Stroke Get(Guid strokeId)
        {
            var stroke = _strokeDataService.Get(strokeId);
            stroke.Lines = _lineDataService.GetByStrockId(strokeId);
            return stroke;
        }
        public Stroke Update(Stroke Stroke)
        {
           return  _strokeDataService.Update(Stroke);
        }


    }
}
