using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Models
{
    public class Line
    {
        public Guid Id { get; set; }
        public Guid StrokeId { get; set; }

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

    }
}
