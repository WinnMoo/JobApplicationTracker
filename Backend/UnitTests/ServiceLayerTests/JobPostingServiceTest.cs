using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ServiceLayerTests
{
    [TestClass]
    public class JobPostingServiceTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        JobPostingService jps;
        MongoClient client;

        public JobPostingServiceTest()
        {
            client = new MongoClient(MONGODB_CONNECTION_STRING);
            jps = new JobPostingService(client);
        }

        [TestMethod]
        public void InsertJobPosting()
        {
            JobPosting jp = new JobPosting("Software Engineer", "Jobtaine", "jobtaine.com/apply");

            var actual = jps.InsertJobPosting(jp);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GetJobPosting()
        {
            var url = "jobtaine.com/apply";

            var retrievedJobPosting = jps.GetJobPosting(url);

            Assert.IsNotNull(retrievedJobPosting);
        }

        [TestMethod]
        public void UpdateJobPosting()
        {
            var url = "jobtaine.com/apply";
            var newJobTitle = "Software Developer";

            var retrievedJobPosting = jps.GetJobPosting(url);

            retrievedJobPosting.JobTitle = newJobTitle;
            var updated = jps.UpdateJobPosting(retrievedJobPosting);
            var retrievedUpdatedJobPosting = jps.GetJobPosting(url);

            Assert.IsTrue(updated);
            Assert.AreEqual(retrievedUpdatedJobPosting.JobTitle, newJobTitle);
        }

        [TestMethod]
        public void DeleteJobPosting()
        {
            var url = "jobtaine.com/apply";

            var deleted = jps.DeleteJobPosting(url);

            Assert.IsTrue(deleted);
        }
    }
}
