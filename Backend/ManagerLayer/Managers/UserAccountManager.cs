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
        private PasswordService _passwordService;


        public UserAccountManager(IMongoDatabase db)
        {
            _userAccountService = new UserAccountService(db);
            _passwordService = new PasswordService();
        }

        public bool CreateUserAccount(AccountRequest request)
        {
            byte[] userSalt = _passwordService.GenerateSalt();
            string hashedPassword = _passwordService.HashPassword(request.Password, userSalt);


            UserAccount newUser = new UserAccount();
            newUser.Email = request.EmailAddress;
            newUser.FirstName = request.FirstName;

            newUser.PasswordHash = hashedPassword;
            newUser.PasswordSalt = userSalt;

            newUser.SecurityAnswer1 = request.SecurityAnswer1;
            newUser.SecurityAnswer2 = request.SecurityAnswer2;
            newUser.SecurityAnswer3 = request.SecurityAnswer3;
            newUser.SecurityQuestion1 = request.SecurityQuestion1;
            newUser.SecurityQuestion2 = request.SecurityQuestion2;
            newUser.SecurityQuestion3 = request.SecurityQuestion3;
            return _userAccountService.InsertUserIntoDB(newUser);
        }

        public bool DeleteUserAccount(LoginRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDB(request.EmailAddress);

            if (_passwordService.validatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                return _userAccountService.DeleteUserFromDB(user.UserAccountId);
            } else
            {
                return false;
            }
        }
    }
}
