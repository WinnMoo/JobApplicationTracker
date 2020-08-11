using ManagerLayer.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ManagerLayerTests
{
    [TestClass]
    public class JobPostingManagerTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);
        JobPostingManager jpm;
        MongoClient client;

        public JobPostingManagerTest()
        {
            client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            jpm = new JobPostingManager(client);
        }


        [TestMethod]
        public void AddJobPosting()
        {

        }

        [TestMethod]
        public void ParseJobPosting()
        {

        }

        [TestMethod]
        public void InsertJobPostingToDB()
        {

        }
    }
}
