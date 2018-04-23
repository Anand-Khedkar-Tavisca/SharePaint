using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Services
{
    public interface ILineDataService
    {
        Line Create(Line line);
        Line Get(Guid lineId);
        IEnumerable<Line> GetByStrockId(Guid strokeId);
    }
}
