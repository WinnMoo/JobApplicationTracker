using ManagerLayer.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ManagerLayerTests
{
    [TestClass]
    public class SessionManagerTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_Test_ConnectionString", EnvironmentVariableTarget.User);

        SessionManager sm;
        MongoClient client;

        public SessionManagerTest()
        {
            client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            sm = new SessionManager(client);
        }

        [TestMethod]
        public void Login()
        {

        }

        [TestMethod]
        public void LogOut()
        {

        }

        [TestMethod]
        public void InvalidateSession()
        {

        }
    }
}
