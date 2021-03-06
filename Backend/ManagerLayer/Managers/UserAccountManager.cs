﻿using DataAccessLayer.Models;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MongoDB.Driver;
using Newtonsoft.Json;
using ServiceLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagerLayer.Managers
{
    public class UserAccountManager
    {
        private const string EMAILADDRESS = "support@jobtaine.com";
        private const string BaseUrl = "www.jobtaine.org";

        private readonly IUserAccountService _userAccountService;
        private readonly PasswordService _passwordService;
        private readonly ResetService _resetService;

        public UserAccountManager(MongoClient _db)
        {
            _userAccountService = new UserAccountService(_db);
            _passwordService = new PasswordService();
            _resetService = new ResetService(_db);
        }

        #region User Accounts
        public ActionResult CreateUserAccount(AccountRequest request)
        {
            if (!EmailService.IsValidEmailAddress(request.EmailAddress.ToLower())) // Checks for valid email address
            {
                return new BadRequestObjectResult("Invalid Email address");
            }

            var retrievedUser = _userAccountService.ReadUserFromDBUsingEmail(request.EmailAddress);
            if (retrievedUser != null) // Checks if user already exists
            {
                //var email = EmailConstructorAccountAlreadyExists();
                //EmailService.SendEmail(email); Commented out until email service is implemented
                return new OkObjectResult("User already exists");
            }

            byte[] userSalt = _passwordService.GenerateSalt();
            string hashedPassword = _passwordService.HashPassword(request.Password, userSalt);

            UserAccount newUser = new UserAccount
            {
                Email = request.EmailAddress.ToLower(),
                FirstName = request.FirstName,

                PasswordHash = hashedPassword,
                PasswordSalt = userSalt,

                SecurityAnswer1 = request.SecurityAnswer1,
                SecurityAnswer2 = request.SecurityAnswer2,
                SecurityAnswer3 = request.SecurityAnswer3,
                SecurityQuestion1 = request.SecurityQuestion1,
                SecurityQuestion2 = request.SecurityQuestion2,
                SecurityQuestion3 = request.SecurityQuestion3
            };

            if (_userAccountService.InsertUserIntoDB(newUser)) // Inserts new user into DB
            {
                /* Commented out until email service is implemented
                 * MimeMessage email = EmailConstructorCreatedAccount("Winn", EMAILADDRESS, request.FirstName, request.EmailAddress);
                var sent = EmailService.SendEmail(email);
                if (!sent)
                {
                    // Use logger service to log not sent email
                }
                 */
                
                return new OkObjectResult("Account successfully created");
            } 
            else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult DeleteUserAccount(LoginRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDBUsingEmail(request.EmailAddress.ToLower());

            if (_passwordService.ValidatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                return new BadRequestObjectResult("Invalid password");
            }
            if (_userAccountService.DeleteUserFromDB(user.UserAccountId))
            {
                var email = EmailConstructorDeletededAccount(user.FirstName, user.Email);
                EmailService.SendEmail(email);
                return new OkObjectResult("User successfully deleted");
            } 
            else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult UpdateUserAccount(AccountRequest request)
        {

            UserAccount user = _userAccountService.ReadUserFromDBUsingEmail(request.EmailAddress.ToLower());

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

        #endregion

        #region Passwords
        public ActionResult UpdateUserPassword(UpdatePasswordRequest request)
        {
            UserAccount user = _userAccountService.ReadUserFromDBUsingEmail(request.EmailAddress.ToLower());

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
                var email = EmailConstructorChangedPassword();
                EmailService.SendEmail(email);
                return new OkObjectResult("Password changed successfully");
            } else
            {
                return new StatusCodeResult(500);
            }
        }

        public ActionResult GenerateResetPasswordToken(string emailAddress)
        {
            MimeMessage email;
            var user = _userAccountService.ReadUserFromDBUsingEmail(emailAddress);
            if(user == null)
            {
                email = EmailConstructorResetUserDoesNotExist();
                EmailService.SendEmail(email);
                return new BadRequestObjectResult("User does not exist");
            }

            // Check if number of password resets generated is < 3
            var tokensGenerated = _resetService.GetTokensByUserId(user.UserAccountId);
            var tokensGeneratedInLast24hrs = tokensGenerated.Where(t => t.DateCreated > DateTime.Now.AddHours(-24));

            if(tokensGeneratedInLast24hrs.Count() >= 3)
            {
                return new BadRequestObjectResult("Unable to generate password reset link, only 3 are allowed per 24hrs. Please try again in 24hrs.");
            }

            string passwordResetToken = CryptoService.GenerateToken();
            PasswordResetToken token = new PasswordResetToken(passwordResetToken, user.UserAccountId);
            _resetService.InsertToken(token);

            string resetLink = BaseUrl + passwordResetToken;

            email = EmailConstructorPasswordResetLink();
            EmailService.SendEmail(email);

            return new OkObjectResult("A password reset link has been sent to your email");
        }

        public ActionResult FetchSecurityQuestions(string PasswordResetToken)
        {
            PasswordResetToken token = _resetService.GetToken(PasswordResetToken);
            if (token == null)
            {
                return new BadRequestObjectResult("Invalid password reset link");
            }
            if (token.Attempts >= 3)
            {
                return new BadRequestObjectResult("Too many attempts have been attempted with this link, please create a new link.");
            }
            if (token.DateCreated.AddMinutes(10) > DateTime.UtcNow)
            {
                return new BadRequestObjectResult("The password reset link has expired, please create a new link.");
            }
            var user = _userAccountService.ReadUserFromDBUsingId(token.UserId);
            Dictionary<string, string> securityQuestions = new Dictionary<string, string>
            {
                { "SecurityQuestion1", user.SecurityQuestion1 },
                { "SecurityQuestion2", user.SecurityQuestion2 },
                { "SecurityQuestion3", user.SecurityQuestion3 }
            };
            return new OkObjectResult(JsonConvert.SerializeObject(securityQuestions, Formatting.Indented));
        }

        public ActionResult CheckSecurityAnswers(SecurityAnswerRequest request)
        {
            // List of steps:
            // Check if password reset is valid (exists in DB) [*]
            // Check how many attempts are left on the token [*]
            // Check creation time of token [*]
            // Check if security answers are valid [*]
            // Update the token (increment attempts, invalidate token if too many attempts etc) [*]
            // Reset the password [*]
            // Send an email notifying user []

            PasswordResetToken token = _resetService.GetToken(request.PasswordResetToken);
            if(token == null)
            {
                return new BadRequestObjectResult("Invalid password reset link");
            }

            if(token.Attempts >= 3)
            {
                return new BadRequestObjectResult("Too many attempts have been attempted with this link, please create a new link.");
            }

            if(token.DateCreated.AddMinutes(10) > DateTime.UtcNow)
            {
                return new BadRequestObjectResult("The password reset link has expired, please create a new link.");
            }

            var user = _userAccountService.ReadUserFromDBUsingId(token.UserId);

            if(user.SecurityAnswer1 != request.SecurityAnswer1 
                || user.SecurityAnswer2 != request.SecurityAnswer2 
                || user.SecurityAnswer3 != request.SecurityAnswer3)
            {
                token.Attempts++;
                _resetService.UpdateToken(token);
                return new BadRequestObjectResult("Security answer(s) are not correct");
            }
            return new OkObjectResult("Able to reset password");
        }

        public ActionResult ResetPassword(ResetPasswordRequest request)
        {
            //TODO: Add password checker to ensure new password conforms with password guidelines
            PasswordResetToken token = _resetService.GetToken(request.PasswordResetToken);
            var user = _userAccountService.ReadUserFromDBUsingId(token.UserId);
            byte[] newUserSalt = _passwordService.GenerateSalt();
            string newHashedPassword = _passwordService.HashPassword(request.NewPassword, newUserSalt);
            user.PasswordSalt = newUserSalt;
            user.PasswordHash = newHashedPassword;

            if (!_userAccountService.UpdateUserInDB(user))
            {
                return new StatusCodeResult(500);
            }

            var email = EmailConstructorPasswordReset();
            EmailService.SendEmail(email);

            return new OkObjectResult("Successfully reset password");
        }
        #endregion

        private MimeMessage EmailConstructorCreatedAccount(string senderName, string senderEmailAddress, string recipientName, string recipientEmailAddress)
        {
            string subject = "Your account has been created!";
            string body = "Hi, \r\n" +
                        "You have created an account on Jobtaine!\r\n" +
                        "https://jobtaine.com/ \r\n\r\n" +
                        "Thanks, Winn";

            return EmailService.CreateEmailPlainBody(senderName, senderEmailAddress, recipientName, recipientEmailAddress, subject, body);
        }

        private MimeMessage EmailConstructorDeletededAccount(string recipientName, string recipientEmailAddress)
        {
            string subject = "Your account has been deleted :(";
            string body = "Hi, \r\n" +
                        "You recently requested to delete your account. \r\n" +
                        "We're sad to see you go, but we wish you well! \r\n" +
                        "Thanks, Winn";

            return EmailService.CreateEmailPlainBody("Winn", "support@jobtaine.com", recipientName, recipientEmailAddress, subject, body);
        }

        private MimeMessage EmailConstructorResetUserDoesNotExist()
        {
            throw new NotImplementedException();
        }
        private MimeMessage EmailConstructorPasswordResetLink()
        {
            throw new NotImplementedException();
        }
        private MimeMessage EmailConstructorPasswordReset()
        {
            throw new NotImplementedException();
        }

        private MimeMessage EmailConstructorChangedPassword()
        {
            throw new NotImplementedException();
        }

        private MimeMessage EmailConstructorAccountAlreadyExists()
        {
            throw new NotImplementedException();
        }
    }
}
