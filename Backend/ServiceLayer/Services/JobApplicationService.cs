using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class JobApplicationService
    {
        private readonly JobApplicationRepository _jobApplicationRepo;
        public JobApplicationService(MongoClient _db)
        {
            _jobApplicationRepo = new JobApplicationRepository(_db);
        }

        public bool InsertJobApplication(JobApplication jobApplication)
        {
            return _jobApplicationRepo.InsertJobApplication(jobApplication).Result;
        }

        public bool DeleteJobApplication(string jobApplicationId)
        {
            return _jobApplicationRepo.DeleteJobApplication(jobApplicationId).Result;
        }

        public bool UpdatejobApplication(JobApplication jobApplication)
        {
            return _jobApplicationRepo.UpdateJobApplication(jobApplication).Result;
        }

        public JobApplication GetJobApplication(string jobApplicationId)
        {
            return _jobApplicationRepo.GetJobApplicationUsingId(jobApplicationId).Result; 
        }

        public JobApplication GetJobApplicationUsingUrl(string url)
        {
            return _jobApplicationRepo.GetJobApplicationUsingUrl(url).Result;
        }

        public List<JobApplication> GetJobApplications(string userId, int startIndex, int numberOfItems)
        {
            return _jobApplicationRepo.GetJobApplications(userId, startIndex, numberOfItems).Result;
        }

        public List<long> GetJobApplicationStatsFunnelGraph(int lengthOfTime, string userAccountId)
        {
            return _jobApplicationRepo.GetJobApplicationStatsFunnelGraph(lengthOfTime, userAccountId).Result;
        }
    }
}
