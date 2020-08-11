using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DataAccessLayerTests
{
    [TestClass]
    public class JobPostingRepositoryTest
    {
        private readonly string MONGODB_TEST_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_Test_ConnectionString", EnvironmentVariableTarget.User);

        readonly MongoClient client;
        readonly JobPostingRepository jpr;

        public JobPostingRepositoryTest()
        {
            client = new MongoClient(MONGODB_TEST_CONNECTION_STRING);
            jpr = new JobPostingRepository(client);
        }

        [TestMethod]
        public void InsertJobPosting()
        {

        }
        [TestMethod]
        public void GetJobPosting()
        {

        }

        [TestMethod]
        public void UpdateJobPosting()
        {

        }

        [TestMethod]
        public void ValidateJobPosting()
        {

        }

        [TestMethod]
        public void DeleteJobPosting()
        {

        }
    }

}
