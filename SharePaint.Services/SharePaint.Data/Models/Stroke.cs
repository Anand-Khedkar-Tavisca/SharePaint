using System;
using System.Collections.Generic;

namespace SharePaint.Data.Models
{
    public class Stroke
    {
        public Guid Id { get; set; }
        public Guid PaintId { get; set; }
        public Guid UserId { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public bool IsUndone{get;set;}
        public IEnumerable<Line> Lines { get; set; }
    }
}
