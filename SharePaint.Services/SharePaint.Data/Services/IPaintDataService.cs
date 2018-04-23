using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Services
{
    public interface IPaintDataService
    {
        Paint Create(Paint paint);
        Paint Get(Guid paintId);


    }
}
