//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SharePaint.Data.Models;
//using SharePaint.Data.Services;

//namespace SharePaintUser.Data.Services
//{
//    public class PaintUserDataServiesProvider : IPaintUserDataService
//    {
//        public static ConcurrentBag<PaintUser> paintUsers;
//        private readonly IUserDataService _userDataService;

//        public PaintUserDataServiesProvider(IUserDataService userDataService)
//        {
//             paintUsers = new ConcurrentBag<PaintUser>();
//             _userDataService = userDataService;
//        }
//        public PaintUser Create(PaintUser paintUser)
//        {
//            paintUsers.Add(paintUser);
//            return paintUser;
//        }

//        public PaintUser Get(Guid paintUserId)
//        {
//            return paintUsers.LastOrDefault(e => e.UserId == paintUserId);
//        }

//        public IEnumerable<UserPublicInfo> GetUsers(Guid paintId)
//        {
//            var users = paintUsers.Where(e=>e.PaintId == paintId).Select(e=>e.UserId).Distinct().Select(e=> _userDataService.Get(e));
//            return users;
//        }
//    }
//}
