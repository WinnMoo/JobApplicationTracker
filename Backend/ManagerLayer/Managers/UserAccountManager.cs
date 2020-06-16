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

        public bool UpdateUserAccount(AccountRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDB(request.EmailAddress);
            if(_passwordService.validatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                user.Email = request.EmailAddress;
                user.FirstName = request.FirstName;
                user.SecurityAnswer1 = request.SecurityAnswer1;
                user.SecurityAnswer2 = request.SecurityAnswer2;
                user.SecurityAnswer3 = request.SecurityAnswer3;
                user.SecurityQuestion1 = request.SecurityQuestion1;
                user.SecurityQuestion2 = request.SecurityQuestion2;
                user.SecurityQuestion3 = request.SecurityQuestion3;
                return _userAccountService.UpdateUserInDB(user);
            } else
            {
                return false;
            }
        }

        public bool UpdateUserPassword(UpdatePasswordRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDB(request.EmailAddress);
            if (_passwordService.validatePassword(request.OldPassword, user.PasswordSalt, user.PasswordHash)) // Check old password
            {
                if(user.SecurityAnswer1 == request.SecurityAnswer1 && user.SecurityAnswer2 == request.SecurityAnswer2 // Check security answers
                    && user.SecurityAnswer3 == request.SecurityAnswer3)
                {
                    byte[] newUserSalt = _passwordService.GenerateSalt();
                    string newHashedPassword = _passwordService.HashPassword(request.NewPassword, newUserSalt);
                    user.PasswordSalt = newUserSalt;
                    user.PasswordHash = newHashedPassword;
                    return _userAccountService.UpdateUserInDB(user);
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
    }
}
