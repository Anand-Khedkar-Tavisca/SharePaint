using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePaint.Services.ServiceProviders
{
    public interface IPaintServies
    {
        Paint CreatePaint(Paint paint);
        Paint CreatePaint();
        Paint CreateOrGetPaint(Guid userId);
    }
}
