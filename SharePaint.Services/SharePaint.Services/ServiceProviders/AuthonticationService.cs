using SharePaint.Data.Models;
using SharePaint.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePaint.Services.ServiceProviders
{
    public class AuthonticationServiceProvider : IAuthonticationService
    {
        IUserDataService _userDataService;
        public AuthonticationServiceProvider(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }
        public bool IsAuthonticate(string tocken, out UserPublicInfo userInfo)
        {
            Guid tkn;
            if (Guid.TryParse(tocken, out tkn))
            {
                userInfo = _userDataService.GetByTocken(tkn);
                return userInfo != null;
            }
            else
            {
                userInfo = null;
                return false;
            }
        }
    }
}
