using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceLayer;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.ServiceLayerTests
{
    [TestClass]
    public class ResetServiceTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_Test_ConnectionString", EnvironmentVariableTarget.User);
        ResetService rs;
        UserAccountService uas;

        ResetServiceTest()
        {
            MongoClient client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            rs = new ResetService(client);
            uas = new UserAccountService(client);
        }

        [TestMethod]
        public void InsertToken()
        {
            var user = uas.ReadUserFromDBUsingEmail("winn@example.org");
            PasswordResetToken token = new PasswordResetToken(CryptoService.GenerateToken(), user.UserAccountId);

            var inserted = rs.InsertToken(token);

            Assert.IsTrue(inserted);
        }

        [TestMethod]
        public void GetTokenByUserId()
        {
            var user = uas.ReadUserFromDBUsingEmail("winn@example.org");

            var tokens = rs.GetTokensByUserId(user.UserAccountId);

            Assert.IsNotNull(tokens);
        }

        [TestMethod]
        public void GetToken()
        {
            var user = uas.ReadUserFromDBUsingEmail("winn@example.org");

            List<PasswordResetToken> tokens = rs.GetTokensByUserId(user.UserAccountId).ToList<PasswordResetToken>();

            var firstToken = tokens[0];

            var retrievedToken = rs.GetToken(firstToken.Token);

            Assert.IsNotNull(retrievedToken);
            Assert.AreEqual(firstToken, retrievedToken);
        }

        [TestMethod]
        public void UpdateToken()
        {
            var attempts = 3;
            var user = uas.ReadUserFromDBUsingEmail("winn@example.org");
            List<PasswordResetToken> tokens = rs.GetTokensByUserId(user.UserAccountId).ToList<PasswordResetToken>();

            var firstToken = tokens[0];

            firstToken.Attempts = attempts;

            rs.UpdateToken(firstToken);

            var retrievedToken = rs.GetToken(firstToken.Token);

            Assert.AreEqual(attempts, retrievedToken.Attempts);
        }

        [TestMethod]
        public void DeleteToken()
        {

        }
    }
}
