using DataAccessLayer.Models;
using MongoDB.Driver;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
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

        public bool CreateUserAccount(AccountRequest request)
        {
            UserAccount newUser = new UserAccount();
            newUser.Email = request.EmailAddress;
            newUser.FirstName = request.FirstName;
            newUser.Password = request.Password; //TODO: hash + salt this
            newUser.SecurityAnswer1 = request.SecurityAnswer1;
            newUser.SecurityAnswer2 = request.SecurityAnswer2;
            newUser.SecurityAnswer3 = request.SecurityAnswer3;
            newUser.SecurityQuestion1 = request.SecurityQuestion1;
            newUser.SecurityQuestion2 = request.SecurityQuestion2;
            newUser.SecurityQuestion3 = request.SecurityQuestion3;
            return _userAccountService.InsertUserIntoDB(newUser);
        }
    }
}
