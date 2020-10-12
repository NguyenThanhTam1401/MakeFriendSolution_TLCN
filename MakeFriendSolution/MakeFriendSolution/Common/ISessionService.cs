using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Common
{
    public interface ISessionService
    {
        void SetSessionUser(AppUser userInfo);

        LoginInfo GetSessionUser();

        public LoginInfo GetDataFromToken();
    }
}