using DataAccessLayer.Models;
using MongoDB.Driver;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Managers
{
    public class UserAccountManager
    {
        private IUserAccountService _userAccountService;

        public UserAccountManager(IMongoDatabase db)
        {
            _userAccountService = new UserAccountService(db);
        }

        public bool CreateUserAccount(UserAccount userAccount)
        {
            return _userAccountService.InsertUserIntoDB(userAccount);
        }
    }
}
