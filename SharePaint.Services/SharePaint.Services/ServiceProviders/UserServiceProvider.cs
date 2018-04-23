using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharePaint.Data.Models;
using SharePaint.Data.Services;

namespace SharePaint.Services.ServiceProviders
{
    public class UserServiceProvider : IUserServies
    {
        private readonly IUserDataService _userDataService;

        public UserServiceProvider(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public UserPublicInfo AddToPaint(Guid userId, Guid paintId)
        {
            return _userDataService.AddUserToPaint(userId, paintId);
        }

        public User CreateUser(UserPublicInfo userPublicInfo)
        {
           return _userDataService.Create(userPublicInfo);
        }

        public User CreateUser()
        {
            return _userDataService.Create(new UserPublicInfo());
        }

        public UserPublicInfo Get(Guid userId)
        {
            return _userDataService.Get(userId);
        }

        public IEnumerable<UserPublicInfo> GetUsersByPaintId(Guid paintId)
        {
            return _userDataService.GetByPaintId(paintId);
        }
    }
}
