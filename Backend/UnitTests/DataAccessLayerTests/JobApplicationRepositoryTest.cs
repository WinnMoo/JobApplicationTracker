using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DataAccessLayerTests
{
    [TestClass]
    public class JobApplicationRepositoryTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_Test_ConnectionString", EnvironmentVariableTarget.User);

        readonly MongoClient client;
        readonly JobApplicationRepository jar;

        public JobApplicationRepositoryTest()
        {
            client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            jar = new JobApplicationRepository(client);
        }

        [TestMethod]
        public void InsertJobApplication()
        {

        }

        [TestMethod]
        public void GetJobApplication_Url()
        {

        }
        [TestMethod]
        public void GetJobApplication_Id()
        {

        }

        [TestMethod]
        public void GetJobApplications()
        {

        }

        [TestMethod]
        public void DeleteJobApplication()
        {

        }

        [TestMethod]
        public void UpdateJobApplication()
        {

        }
    }
}
