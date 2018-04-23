using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Models
{
    public class Paint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Stroke> Strokes { get; set; }
    }
}
