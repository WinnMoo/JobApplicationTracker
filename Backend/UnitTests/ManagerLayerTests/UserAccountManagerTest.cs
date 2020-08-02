using ManagerLayer.Managers;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceLayer.Services;
using System;

namespace UnitTests.ManagerLayerTests
{
    [TestClass]
    public class UserAccountManagerTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);
        readonly TestUtilities tu;
        readonly UserAccountManager uam;
        readonly UserAccountService uas;
        readonly MongoClient client;

        public UserAccountManagerTest()
        {
            client = new MongoClient(MONGODB_CONNECTION_STRING);
            uam = new UserAccountManager(client);
            uas = new UserAccountService(client);
            tu = new TestUtilities();
        }

        [TestMethod]
        public void CreateAccount_Fail_BadEmail()
        {
            var badEmail = "winn@asdf";
            var expected = new BadRequestObjectResult("Invalid Email address");

            var request = tu.CreateAccountRequest();
            request.EmailAddress = badEmail;

            var actual = uam.CreateUserAccount(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateAccount_Fail_AccountAlreadyExists()
        {
            var badEmail = "winn@example.org";
            var expected = new OkObjectResult("User already exists");

            var request = tu.CreateAccountRequest();
            request.EmailAddress = badEmail;

            var actual = uam.CreateUserAccount(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateAccount_Pass()
        {
            var expected = new OkObjectResult("Account successfully created");

            var request = tu.CreateAccountRequest();

            var actual = uam.CreateUserAccount(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserAccount_Fail_UserDoesNotExist()
        {
            var badEmail = "bob@example.org";
            var expected = new BadRequestObjectResult("User does not exist");

            var request = tu.CreateAccountRequest();
            request.EmailAddress = badEmail;

            var actual = uam.UpdateUserAccount(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserAccount_Fail_WrongPassword()
        {
            var badPassword = "password1";
            var expected = new BadRequestObjectResult("User does not exist");

            var request = tu.CreateAccountRequest();
            request.Password = badPassword;

            var actual = uam.UpdateUserAccount(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserAccount_Pass()
        {
            var expected = new OkObjectResult("User successfully updated");
            var newSecurityAnswer = "SecurityAnswer4";

            var request = tu.CreateAccountRequest();
            request.SecurityQuestion3 = newSecurityAnswer;

            //Get user from db and compare new updated info
            var actual = uam.UpdateUserAccount(request);
            var retrievedUpdatedAccount = uas.ReadUserFromDB("winn@example.org");

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(newSecurityAnswer, retrievedUpdatedAccount.SecurityAnswer3);
        }

        [TestMethod]
        public void DeleteUserAccountz_Fail_BadPassword()
        {
            var badPassword = "password1";
            var expected = new BadRequestObjectResult("Invalid password");

            LoginRequest request = new LoginRequest
            {
                EmailAddress = "winn@example.org",
                Password = badPassword
            };

            var actual = uam.DeleteUserAccount(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserPassword_Fail_UserDoesNotExist()
        {
            var request = tu.CreateUpdatePasswordRequest();
            request.EmailAddress = "bob@example.org";
            var expected = new BadRequestObjectResult("User does not exist");

            var actual = uam.UpdateUserPassword(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserPassword_Fail_Password()
        {
            var request = tu.CreateUpdatePasswordRequest();
            var badPassword = "password1";
            request.OldPassword = badPassword;
            var expected = new BadRequestObjectResult("Password is incorrect");

            var actual = uam.UpdateUserPassword(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserPassword_Fail_WrongSecurityAnswer()
        {
            var request = tu.CreateUpdatePasswordRequest();
            var badSecurityAnswer = "SecurityAnswer2";
            request.SecurityAnswer1 = badSecurityAnswer;
            var expected = new BadRequestObjectResult("Security answer(s) incorrect");

            var actual = uam.UpdateUserPassword(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateUserPassword_Pass()
        {
            var request = tu.CreateUpdatePasswordRequest();
            var expected = new OkObjectResult("Password changed successfully");

            var actual = uam.UpdateUserPassword(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GeneratePasswordReset_Fail_UserDoesNotExist()
        {
            var expected = new BadRequestObjectResult("User does not exist");
            var badEmail = "winn.me@example.org";

            var actual = uam.GenerateResetPassword(badEmail);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GeneratePasswordReset_Fail_TooManyAttempts()
        {
            var emailAddress = "winnmoo@gmail.com";
            uam.GenerateResetPassword(emailAddress);
            uam.GenerateResetPassword(emailAddress);
            uam.GenerateResetPassword(emailAddress);
            var expected = new BadRequestObjectResult("Unable to generate password reset link, only 3 are allowed per 24hrs. Please try again in 24hrs.");

            var actual = uam.GenerateResetPassword(emailAddress);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GeneratePasswordReset_Pass()
        {
            var expected = new OkObjectResult("A password reset link has been sent to your email");
        }

        [TestMethod]
        public void ResetPassword_Fail_InvalidLink()
        {
            var expected = new BadRequestObjectResult("Invalid password reset link");
        }
        [TestMethod]
        public void ResetPassword_Fail_TooManyAttempts()
        {
            var expected = new BadRequestObjectResult("Too many attempts have been attempted with this link, please create a new link.");
        }
        [TestMethod]
        public void ResetPassword_Fail_ExpiredLink()
        {
            var expected = new BadRequestObjectResult("The password reset link has expired, please create a new link.");

        }
        [TestMethod]
        public void ResetPassword_Fail_InvalidSecurityAnswers()
        {
            var expected = new BadRequestObjectResult("Security answer(s) are not correct");
        }
        [TestMethod]
        public void ResetPassword_Pass()
        {
            var expected = new OkObjectResult("Successfully reset password");
        }

        [TestMethod]
        public void DeleteUserAccountz_Pass()
        {
            var expected = new BadRequestObjectResult("Invalid password");

            LoginRequest request = new LoginRequest
            {
                EmailAddress = "winn@example.org",
                Password = "password"
            };

            var actual = uam.DeleteUserAccount(request);

            Assert.AreEqual(expected, actual);
        }
    }
}
