using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer;
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

        private string BaseUrl = "";


        public UserAccountManager(MongoClient dbClient)
        {
            _userAccountService = new UserAccountService(dbClient);
            _passwordService = new PasswordService();
        }

        public ActionResult CreateUserAccount(AccountRequest request)
        {
            if (!EmailService.IsValidEmailAddress(request.EmailAddress)) // Checks for valid email address
            {
                return new BadRequestObjectResult("Invalid Email address");
            }

            if(_userAccountService.ReadUserFromDB(request.EmailAddress) != null) // Checks if user already exists
            {
                return new BadRequestObjectResult("User Already Exists with this Email Address");
            }

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

            // TODO: Send email confirmation

            if (_userAccountService.InsertUserIntoDB(newUser)) // Inserts new user into DB
            {
                return new OkObjectResult(true);
            } 
            else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult DeleteUserAccount(LoginRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDB(request.EmailAddress);

            // TODO: Send email confirmation

            if (_passwordService.ValidatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                return new BadRequestObjectResult("Invalid password");
            }
            if (_userAccountService.DeleteUserFromDB(user.UserAccountId))
            {
                return new OkObjectResult("User successfully deleted");
            } 
            else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult UpdateUserAccount(AccountRequest request)
        {

            UserAccount user = _userAccountService.ReadUserFromDB(request.EmailAddress);

            if(user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }
            if (!_passwordService.ValidatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                return new BadRequestObjectResult("Invalid Password");
            }

            user.Email = request.EmailAddress;
            user.FirstName = request.FirstName;
            user.SecurityAnswer1 = request.SecurityAnswer1;
            user.SecurityAnswer2 = request.SecurityAnswer2;
            user.SecurityAnswer3 = request.SecurityAnswer3;
            user.SecurityQuestion1 = request.SecurityQuestion1;
            user.SecurityQuestion2 = request.SecurityQuestion2;
            user.SecurityQuestion3 = request.SecurityQuestion3;
            if (_userAccountService.UpdateUserInDB(user))
            {
                return new OkObjectResult("User successfully updated");
            } 
            else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult UpdateUserPassword(UpdatePasswordRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDB(request.EmailAddress);

            // TODO: Send email confirmation

            if (user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }

            if (!_passwordService.ValidatePassword(request.OldPassword, user.PasswordSalt, user.PasswordHash)) // Check old password
            {
                return new BadRequestObjectResult("Password is incorrect");
            }

            if (user.SecurityAnswer1 != request.SecurityAnswer1 || user.SecurityAnswer2 != request.SecurityAnswer2 // Check security answers
                    || user.SecurityAnswer3 != request.SecurityAnswer3)
            {
                return new BadRequestObjectResult("Security answer(s) incorrect");
            }

            byte[] newUserSalt = _passwordService.GenerateSalt();
            string newHashedPassword = _passwordService.HashPassword(request.NewPassword, newUserSalt);
            user.PasswordSalt = newUserSalt;
            user.PasswordHash = newHashedPassword;

            if (_userAccountService.UpdateUserInDB(user))
            {
                return new OkObjectResult("Password changed successfully");
            } else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult GenerateResetPassword(string emailAddress)
        {
            var user = _userAccountService.ReadUserFromDB(emailAddress);
            if(user == null)
            {
                // TODO: send email to user about someone attempting to reset your account
                return new BadRequestObjectResult("User does not exist");
            }
            string passwordResetToken = CryptoService.GenerateToken();
            //TODO: Add token to DB

            string resetLink = BaseUrl + passwordResetToken;

            //TODO: send email to user with reset link

            return new OkObjectResult("A password reset link has been sent to your email");
        }

        public ActionResult ResetPassword()
        {
            throw new NotImplementedException();
        }
    }
}
