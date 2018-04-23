using SharePaint.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePaint.Data.Services
{
    public interface IUserDataService
    {
        User Create(UserPublicInfo user);
        UserPublicInfo AddUserToPaint(Guid userId, Guid paintId);

        UserPublicInfo Get(Guid userId);
        UserPublicInfo GetByTocken(Guid secret);
        IEnumerable<UserPublicInfo> GetByPaintId(Guid paintId);
    }
}
