using DataAccessLayer.Models;
using ManagerLayer.Requests;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UnitTests
{
    public class TestUtilities
    {
        public byte[] GenerateRandomBytes()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public UserAccount CreateUser()
        {
            var user = new UserAccount
            {
                FirstName = "Winn",
                Email = "winn@example.org",
                PasswordHash = (Guid.NewGuid()).ToString(),
                PasswordSalt = GenerateRandomBytes(),
                SecurityQuestion1 = "SecurityQuestion1",
                SecurityQuestion2 = "SecurityQuestion2",
                SecurityQuestion3 = "SecurityQuestion3",
                SecurityAnswer1 = "SecurityAnswer1",
                SecurityAnswer2 = "SecurityAnswer1",
                SecurityAnswer3 = "SecurityAnswer1"
            };
            return user;
        }

        public UpdatePasswordRequest CreateUpdatePasswordRequest()
        {
            var request = new UpdatePasswordRequest
            {
                EmailAddress = "winn@example.org",
                OldPassword = "password",
                NewPassword = "password1",
                SecurityAnswer1 = "SecurityAnswer1",
                SecurityAnswer2 = "SecurityAnswer2",
                SecurityAnswer3 = "SecurityAnswer3"
            };

            return request;
        }

        public AccountRequest CreateAccountRequest()
        {
            AccountRequest request = new AccountRequest
            {
                EmailAddress = "winn@example.org",
                FirstName = "Winn",
                Password = "password",
                SecurityQuestion1 = "SecurityQuestion1",
                SecurityQuestion2 = "SecurityQuestion2",
                SecurityQuestion3 = "SecurityQuestion3",
                SecurityAnswer1 = "SecurityAnswer1",
                SecurityAnswer2 = "SecurityAnswer2",
                SecurityAnswer3 = "SecurityAnswer3"
            };
            return request;
        }


        public SecurityAnswerRequest CreateSecurityAnswersRequest()
        {
            SecurityAnswerRequest request = new SecurityAnswerRequest
            {
                PasswordResetToken = "",
                SecurityAnswer1 = "SecurityAnswer1",
                SecurityAnswer2 = "SecurityAnswer2",
                SecurityAnswer3 = "SecurityAnswer3"
            };
            return request;
        }

        public ResetPasswordRequest CreateResetPasswordRequest()
        {
            ResetPasswordRequest request = new ResetPasswordRequest
            {
                PasswordResetToken = "",
                NewPassword = "Password1!"
            };
            return request;
        }
    }
}
