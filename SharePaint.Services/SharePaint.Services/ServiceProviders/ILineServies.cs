using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareStroke.Services.ServiceProviders
{
    public interface ILineService
    {
        Line CreateLine(Line line);
        IEnumerable<Line> GetByStrokeId(Guid strokeId);
    }
}
