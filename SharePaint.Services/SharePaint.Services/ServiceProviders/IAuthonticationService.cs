using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePaint.Services.ServiceProviders
{
    public interface IAuthonticationService
    {
        bool IsAuthonticate(string tocken, out UserPublicInfo userInfo);
    }
}
