using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Services
{
    public interface IStrokeDataService
    {
        Stroke Create(Stroke stroke);
        Stroke Get(Guid stokeId);
        IEnumerable<Stroke> GetByPaintId(Guid paintId);
        Stroke Update(Stroke Stroke);

    }
}
