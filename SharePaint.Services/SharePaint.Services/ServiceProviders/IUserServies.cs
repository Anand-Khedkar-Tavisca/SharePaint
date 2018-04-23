using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePaint.Services.ServiceProviders
{
    public interface IUserServies
    {
        User CreateUser(UserPublicInfo userPublicInfo);
        User CreateUser();

        UserPublicInfo Get(Guid userId);
        UserPublicInfo AddToPaint(Guid userId,Guid paintId);
        IEnumerable<UserPublicInfo> GetUsersByPaintId(Guid paintId);

    }
}
