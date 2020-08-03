using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DataAccessLayerTests
{
    [TestClass]
    public class ResetTokenRepositoryTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        readonly MongoClient client;
        readonly ResetTokenRepository rtr;

        public ResetTokenRepositoryTest()
        {
            client = new MongoClient(MONGODB_CONNECTION_STRING);
            rtr = new ResetTokenRepository(client);
        }

        [TestMethod]
        public void GetToken()
        {

        }
        [TestMethod]
        public void GetTokens()
        {

        }
        [TestMethod]
        public void InsertToken()
        {

        }

        [TestMethod]
        public void UpdateToken()
        {

        }
    }
}
