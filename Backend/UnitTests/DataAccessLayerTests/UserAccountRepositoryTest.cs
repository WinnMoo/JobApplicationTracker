using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DataAccessLayerTests
{
    [TestClass]
    public class UserAccountRepositoryTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_Test_ConnectionString", EnvironmentVariableTarget.User);

        readonly MongoClient client;
        readonly UserAccountRepository uar;

        public UserAccountRepositoryTest()
        {
            client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            uar = new UserAccountRepository(client);
        }

        [TestMethod]
        public void InsertUserAccount()
        {

        }

        [TestMethod]
        public void GetUserAccount_AccountId()
        {

        }

        [TestMethod]
        public void GetUserAccount_EmailAddress()
        {

        }

        [TestMethod]
        public void UpdateUserAccount()
        {

        }

        [TestMethod]
        public void DeleteUserAccount()
        {

        }
    }
}
