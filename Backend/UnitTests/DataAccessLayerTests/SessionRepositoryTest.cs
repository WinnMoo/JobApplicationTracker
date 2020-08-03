using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DataAccessLayerTests
{
    [TestClass]
    public class SessionRepositoryTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        readonly MongoClient client;
        readonly SessionRepository sr;

        public SessionRepositoryTest()
        {
            client = new MongoClient(MONGODB_CONNECTION_STRING);
            sr = new SessionRepository(client);
        }

        [TestMethod]
        public void AddSession()
        {

        }

        [TestMethod]
        public void GetSession()
        {

        }
        [TestMethod]
        public void DeleteSession()
        {

        }

        [TestMethod]
        public void UpdateSession()
        {

        }

        [TestMethod]
        public void ExpireAllSessions()
        {

        }
    }
}
