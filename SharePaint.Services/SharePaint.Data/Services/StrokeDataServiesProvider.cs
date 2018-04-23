using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SharePaint.Data.Models;
using SharePaint.Data.Services;

namespace ShareStroke.Data.Services
{
    public class StrokeDataServiesProvider : IStrokeDataService
    {
        public static ConcurrentDictionary<Guid, Stroke> Strokes;

        public StrokeDataServiesProvider()
        {
             Strokes = new ConcurrentDictionary<Guid, Stroke>();
        }
        public Stroke Create(Stroke Stroke)
        {
            var id = Stroke.Id = Guid.NewGuid();
            Strokes.TryAdd(id, Stroke);
            return Stroke;
        }

        public Stroke Update(Stroke Stroke)
        {
            //Stroke.IsUndone = true;
            Strokes.AddOrUpdate(Stroke.Id, Stroke, (key,oldValue)=> { return Stroke; });
            return Stroke;
        }


        public Stroke Get(Guid StrokeId)
        {
            Stroke Stroke;
            Strokes.TryGetValue(StrokeId, out Stroke);
            return Stroke;
        }
        public IEnumerable<Stroke> GetByPaintId(Guid paintId)
        {
            var strokes = Strokes.Where(e => e.Value.PaintId == paintId).Select(e => e.Value);

            return strokes;
        }

    }
}
