using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UnitTests.ServiceLayerTests
{
    [TestClass]
    public class UserAccountServiceTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);
        TestUtilities tu;
        UserAccountService uas;
        public UserAccountServiceTest()
        {
            MongoClient client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            uas = new UserAccountService(client);
            tu = new TestUtilities();
        }

        [TestMethod]
        public void InsertUser()
        {
            var user = tu.CreateUser();

            var inserted = uas.InsertUserIntoDB(user);

            Assert.IsTrue(inserted);
        }

        [TestMethod]
        public void ReadUser_Id()
        {
            var emailAddress = "winn@example.org";

            var userRetrievedByEmail = uas.ReadUserFromDBUsingEmail(emailAddress);

            var userRetrievedById = uas.ReadUserFromDBUsingEmail(userRetrievedByEmail.UserAccountId);

            Assert.IsNotNull(userRetrievedById);
            Assert.AreEqual(userRetrievedByEmail, userRetrievedById);
        }

        [TestMethod]
        public void ReadUser_EmailAddress()
        {
            var emailAddress = "winn@example.org";

            var user = uas.ReadUserFromDBUsingEmail(emailAddress);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UpdateUser()
        {
            var emailAddress = "winn@example.org";

            var userRetrievedByEmail = uas.ReadUserFromDBUsingEmail(emailAddress);

            userRetrievedByEmail.FirstName = "Bob";

            var updated = uas.UpdateUserInDB(userRetrievedByEmail);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public void DeleteUser()
        {
            var emailAddress = "winn@example.org";

            var user = uas.ReadUserFromDBUsingEmail(emailAddress);
            var deleted = uas.DeleteUserFromDB(user.UserAccountId);

            Assert.IsTrue(deleted);
        }
    }
}
