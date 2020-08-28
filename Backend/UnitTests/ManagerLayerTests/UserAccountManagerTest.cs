using DataAccessLayer.Models;
using ManagerLayer.Managers;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceLayer;
using ServiceLayer.Services;
using System;
using System.Linq;

namespace UnitTests.ManagerLayerTests
{
    [TestClass]
    public class UserAccountManagerTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_Test_ConnectionString", EnvironmentVariableTarget.User);
        readonly TestUtilities tu;
        readonly UserAccountManager uam;
        readonly UserAccountService uas;
        readonly MongoClient client;
        readonly ResetService rs;

        public UserAccountManagerTest()
        {
            client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            uam = new UserAccountManager(client);
            uas = new UserAccountService(client);
            tu = new TestUtilities();
            rs = new ResetService(client);
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

            var actual = uam.GenerateResetPasswordToken(badEmail);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GeneratePasswordReset_Fail_TooManyAttempts()
        {
            var emailAddress = "winn@example.com";
            uam.GenerateResetPasswordToken(emailAddress);
            uam.GenerateResetPasswordToken(emailAddress);
            uam.GenerateResetPasswordToken(emailAddress);
            var expected = new BadRequestObjectResult("Unable to generate password reset link, only 3 are allowed per 24hrs. Please try again in 24hrs.");

            var actual = uam.GenerateResetPasswordToken(emailAddress);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GeneratePasswordReset_Pass()
        {
            // Arrange
            var emailAddress = "winn@example.org";
            var expected = new OkObjectResult("A password reset link has been sent to your email");
            // Clear all previously created tokens
            var tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId);
            foreach(var token in tokens)
            {
                rs.DeleteToken(token.Token);
            }
            
            // Act
            var actual = uam.GenerateResetPasswordToken(emailAddress);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        // TODO: Add Unit Tests for fetching security questions
        [TestMethod]
        public void FetchSecurityQuestions_Fail_InvalidLink()
        {

        }

        [TestMethod]
        public void FetchSecurityQuestions_Fail_TooManyAttempts()
        {

        }

        [TestMethod]
        public void FetchSecurityQuestions_Fail_ExpiredToken()
        {

        }

        [TestMethod]
        public void FetchSecurityQuestions_Pass()
        {

        }

        [TestMethod]
        public void CheckSecurityAnswers_Fail_InvalidLink()
        {
            var token = CryptoService.GenerateToken();
            var expected = new BadRequestObjectResult("Invalid password reset link");

            var request = tu.CreateSecurityAnswersRequest();
            request.PasswordResetToken = token;

            var actual = uam.CheckSecurityAnswers(request);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckSecurityAnswers_Fail_TooManyAttempts()
        {
            //Arrange
            var emailAddress = "winn@example.org";
            var expected = new BadRequestObjectResult("Too many attempts have been attempted with this link, please create a new link.");
            var tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId);
            foreach (var token in tokens)
            {
                rs.DeleteToken(token.Token);
            }
            uam.GenerateResetPasswordToken(emailAddress);
            tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId);
            var tokensAsList = tokens.ToList<PasswordResetToken>();
            var generatedToken = tokensAsList[0];

            //Act
            var request = tu.CreateSecurityAnswersRequest();
            request.PasswordResetToken = generatedToken.Token;
            request.SecurityAnswer1 = "SecurityAnswer2";

            uam.CheckSecurityAnswers(request);
            uam.CheckSecurityAnswers(request);
            uam.CheckSecurityAnswers(request);
            var actual = uam.CheckSecurityAnswers(request);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckSecurityAnswers_Fail_ExpiredToken()
        {
            //Arrange
            var emailAddress = "winn@example.org";
            var expected = new BadRequestObjectResult("The password reset link has expired, please create a new link.");
            var tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId); // Clears all previously created tokens
            foreach (var token in tokens)
            {
                rs.DeleteToken(token.Token);
            }

            uam.GenerateResetPasswordToken(emailAddress); // Generates a "fresh" token
            tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId);
            var tokensAsList = tokens.ToList<PasswordResetToken>();
            var generatedToken = tokensAsList[0];

            generatedToken.DateCreated = DateTime.UtcNow.AddMinutes(-60); // Updates the token creation time to be 1 hour previous from now
            rs.UpdateToken(generatedToken);

            var request = tu.CreateSecurityAnswersRequest();
            request.PasswordResetToken = generatedToken.Token;

            //Act
            var actual = uam.CheckSecurityAnswers(request);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckSecurityAnswers_Fail_InvalidSecurityAnswers()
        {
            var emailAddress = "winn@example.org";
            var request = tu.CreateSecurityAnswersRequest();
            request.SecurityAnswer1 = "SecurityAnswer10";
            var expected = new BadRequestObjectResult("Security answer(s) are not correct");
            var tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId); // Clears all previously created tokens
            foreach (var token in tokens)
            {
                rs.DeleteToken(token.Token);
            }

            uam.GenerateResetPasswordToken(emailAddress);
            tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId);
            var tokensAsList = tokens.ToList<PasswordResetToken>();
            var generatedToken = tokensAsList[0];
            request.PasswordResetToken = generatedToken.Token;

            var actual = uam.CheckSecurityAnswers(request);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckSecurityAnswers_Pass()
        {
            var emailAddress = "winn@example.org";
            var request = tu.CreateSecurityAnswersRequest();
            var expected = new OkObjectResult("Successfully reset password");

            var tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId); // Clears all previously created tokens
            foreach (var token in tokens)
            {
                rs.DeleteToken(token.Token);
            }

            uam.GenerateResetPasswordToken(emailAddress);
            tokens = rs.GetTokensByUserId(uas.ReadUserFromDB(emailAddress).UserAccountId);
            var tokensAsList = tokens.ToList<PasswordResetToken>();
            var generatedToken = tokensAsList[0];
            request.PasswordResetToken = generatedToken.Token;

            var actual = uam.CheckSecurityAnswers(request);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteUserAccount_Pass()
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

        //TODO: Add unit tests for resetting password
        [TestMethod]
        public void ResetPassword_Fail_NonConformingPassword()
        {

        }

        [TestMethod]
        public void ResetPassword_Pass()
        {

        }

    }
}
