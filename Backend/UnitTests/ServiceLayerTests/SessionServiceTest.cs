using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceLayer;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ServiceLayerTests
{
    [TestClass]
    public class SessionServiceTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        SessionService ss;

        public SessionServiceTest()
        {
            MongoClient client = new MongoClient(MONGODB_CONNECTION_STRING);
            ss = new SessionService(client);
        }

        [TestMethod]
        public void AddSession()
        {
            var token = CryptoService.GenerateToken();
            var session = new Session("winn@example.org", token);

            var added = ss.AddSession(session);

            Assert.IsTrue(added);
        }

        [TestMethod]
        public void GetSession()
        {
            var emailAddress = "winn@example.org";

            var session = ss.GetSession(emailAddress);

            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void UpdateSession()
        {
            // Arrange
            var session = ss.GetSession("winn@example.org");
            var now = DateTime.UtcNow;

            session.DateExpired = now;

            // Act
            var updated = ss.UpdateSession(session);

            var retrievedSession = ss.GetSession("winn@example.org");

            //Assert
            Assert.AreEqual(now, retrievedSession.DateExpired);
            Assert.IsTrue(updated);
        }

        [TestMethod]
        public void DeleteSession()
        {
            // Arrange
            var emailAddress = "winn@example.org";

            var session = ss.GetSession(emailAddress);

            // Act
            var deleted = ss.DeleteSession(session);

            //Assert
            Assert.IsTrue(deleted);
        }
    }
}
