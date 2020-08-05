using ManagerLayer.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ManagerLayerTests
{
    [TestClass]
    public class JobApplicationManagerTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        JobApplicationManager jam;
        MongoClient client;

        public JobApplicationManagerTest()
        {
            client = new MongoClient(MONGODB_CONNECTION_STRING);
            jam = new JobApplicationManager(client);
        }
        

        [TestMethod]
        public void AddJobApplication()
        {

        }

        [TestMethod]
        public void UpdateJobApplication()
        {

        }

        [TestMethod]
        public void GetJobApplication()
        {

        }

        [TestMethod]
        public void DeleteJobApplication()
        {

        }
    }
}
