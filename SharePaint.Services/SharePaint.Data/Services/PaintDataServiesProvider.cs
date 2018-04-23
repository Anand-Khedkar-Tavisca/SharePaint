using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using SharePaint.Data.Models;

namespace SharePaint.Data.Services
{
    public class PaintDataServiesProvider : IPaintDataService
    {
        public static ConcurrentDictionary<Guid, Paint> paints;

        public PaintDataServiesProvider()
        {
             paints = new ConcurrentDictionary<Guid, Paint>();
        }
        public Paint Create(Paint paint)
        {
            var id = paint.Id = Guid.NewGuid();
            paints.TryAdd(id, paint);
            return paint;
        }

        public Paint Get(Guid paintId)
        {
            Paint paint;
            paints.TryGetValue(paintId, out paint);
            return paint;
        }


    }
}
