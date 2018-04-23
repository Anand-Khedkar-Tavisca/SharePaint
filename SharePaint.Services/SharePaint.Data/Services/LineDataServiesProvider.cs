using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SharePaint.Data.Models;
using SharePaint.Data.Services;

namespace ShareLine.Data.Services
{
    public class LineDataServiesProvider : ILineDataService
    {
        public static ConcurrentDictionary<Guid, Line> Lines;

        public LineDataServiesProvider()
        {
             Lines = new ConcurrentDictionary<Guid, Line>();
        }
        public Line Create(Line Line)
        {
            var id = Line.Id = Guid.NewGuid();
            Lines.TryAdd(id, Line);
            return Line;
        }

        public Line Get(Guid LineId)
        {
            Line Line;
            Lines.TryGetValue(LineId, out Line);
            return Line;
        }

        public IEnumerable<Line> GetByStrockId(Guid strokeId)
        {
           return Lines.Where(e => e.Value.StrokeId == strokeId).Select(e=>e.Value);
        }
             


    }
}
