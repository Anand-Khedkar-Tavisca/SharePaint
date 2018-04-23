using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SharePaint.Data.Models;
using SharePaint.Data.Services;

namespace ShareUser.Data.Services
{
    public class UserDataServiesProvider : IUserDataService
    {
        public static ConcurrentDictionary<Guid, User> users;

        public UserDataServiesProvider()
        {
             users = new ConcurrentDictionary<Guid, User>();
        }

        public UserPublicInfo AddUserToPaint(Guid userId, Guid paintId)
        {
           var user =  users.FirstOrDefault(e => e.Key == userId).Value;
            user.PaintId = paintId;
            return user;
        }

        public User Create(UserPublicInfo user)
        {
            var id  = Guid.NewGuid();
            var newUser = new User
            {
                Id = id,
                Name = user.Name,
                Address = user.Address,
                Secret = Guid.NewGuid()
            };
            users.TryAdd(id, newUser);
            return newUser;
        }

        public UserPublicInfo Get(Guid userId)
        {
            User user;
            users.TryGetValue(userId, out user);
            return user;
        }

        public UserPublicInfo GetByTocken(Guid secret)
        {
            return users.FirstOrDefault(e=>e.Value.Secret == secret).Value;
        }

        public IEnumerable<UserPublicInfo> GetByPaintId(Guid paintId)
        {
            return users.Where(e => e.Value.PaintId == paintId).Select(e=>e.Value);
        }
    }
}
