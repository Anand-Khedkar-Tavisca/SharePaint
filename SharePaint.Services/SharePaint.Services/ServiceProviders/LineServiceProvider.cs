using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharePaint.Data.Models;
using SharePaint.Data.Services;
using ShareLine.Data.Services;
using ShareStroke.Services.ServiceProviders;

namespace ShareLine.Services.ServiceProviders
{
    public class LineServiceProvider : ILineService
    {
        private readonly ILineDataService _lineDataService;
        private readonly IUserDataService _userDataService;

        public LineServiceProvider(ILineDataService lineDataService, IUserDataService userDataService)
        {
            _lineDataService = lineDataService;
            _userDataService = userDataService;
        }
        public Line CreateLine(Line line)
        {
           return _lineDataService.Create(line);
        }

        public IEnumerable<Line> GetByStrokeId(Guid strokeId)
        {
            return _lineDataService.GetByStrockId(strokeId);
        }

      
    }
}
