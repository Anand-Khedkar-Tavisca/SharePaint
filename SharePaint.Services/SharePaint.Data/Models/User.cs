using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Models
{
    public class User : UserPublicInfo
    {
        public Guid Secret { get; set; }
    }

    public class UserPublicInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid PaintId { get; set; }
    }
}
