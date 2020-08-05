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
    public class JobApplicationServiceTest
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);
        JobApplicationService jas;
        public JobApplicationServiceTest()
        {
            MongoClient client = new MongoClient(MONGODB_CONNECTION_STRING); 
            jas = new JobApplicationService(client);
        }


        [TestMethod]
        public void InsertJobApplication()
        {
            //Arrange
            var jobApplication = new JobApplication{
                CompanyName = "Jobtaine",
                JobTitle = "Software Engineer",
                City = "Westminster",
                State = "CA",
                JobPostingURL = "jobtaine.com/jobapplication",
                UserDefinedFields = new Dictionary<string, string>()
            };

            //Act
            bool actual = jas.InsertJobApplication(jobApplication);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GetJobApplication_Url()
        {
            //Arrange
            var jobApplication = new JobApplication
            {
                CompanyName = "Jobtaine",
                JobTitle = "Software Engineer",
                City = "Westminster",
                State = "CA",
                JobPostingURL = "jobtaine.com/jobapplication",
                UserDefinedFields = new Dictionary<string, string>()
            };

            //Act
            var actual = jas.GetJobApplication("jobtaine.com/jobapplication");

            //Assert
            Assert.AreEqual(jobApplication, actual);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void UpdateJobApplication()
        {
            //Arrange
            var jobApplication = jas.GetJobApplication("jobtaine.com/jobapplication");
            jobApplication.CompanyName = "NotJobtaine";

            var actual = jas.UpdatejobApplication(jobApplication);

            var retrievedApplication = jas.GetJobApplication("jobtaine.com/jobapplication");

            Assert.IsTrue(actual);
            Assert.AreEqual(retrievedApplication.CompanyName, "NotJobtaine");
        }

        [TestMethod]
        public void GetJobApplications()
        {
            
        }

        [TestMethod]
        public void DeleteJobApplication()
        {
            var retrievedApplication = jas.GetJobApplication("greetngroup.com/jobapplication");

            var deleted = jas.DeleteJobApplication(retrievedApplication.JobApplicationId);

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public void GetJobApplication_JobId()
        {
            //Arrange
            var jobApplication = new JobApplication
            {
                CompanyName = "GreetNGroup",
                JobTitle = "Software Engineer",
                City = "Long Beach",
                State = "CA",
                JobPostingURL = "greetngroup.com/jobapplication",
                UserDefinedFields = new Dictionary<string, string>()
            };
            jas.InsertJobApplication(jobApplication);
            //Act
            jobApplication = jas.GetJobApplication("greetngroup.com/jobapplication");
            var retrievedApplication = jas.GetJobApplication(jobApplication.JobApplicationId);


            //Assert
            Assert.IsNotNull(retrievedApplication);
        }
    }
}
