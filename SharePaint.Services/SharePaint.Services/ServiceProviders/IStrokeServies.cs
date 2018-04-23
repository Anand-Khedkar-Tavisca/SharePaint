using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareStroke.Services.ServiceProviders
{
    public interface IStrokeService
    {
        Stroke CreateStroke(Stroke Stroke);
        Stroke Get(Guid strokeId);
        Stroke Update(Stroke Stroke);
    }
}
