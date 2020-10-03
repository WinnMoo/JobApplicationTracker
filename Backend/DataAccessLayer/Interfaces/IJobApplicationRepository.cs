using DataAccessLayer.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    interface IJobApplicationRepository
    {
        public Task<bool> InsertJobApplication(JobApplication jobApplication);
        public Task<JobApplication> GetJobApplicationUsingId(string jobApplicationId);
        public Task<JobApplication> GetJobApplicationUsingUrl(string jobPostingURL);
        public Task<List<JobApplication>> GetJobApplications(string jobApplicationId, int startIndex, int numberOfItmes);
        public Task<bool> DeleteJobApplication(string jobApplicationId);
        public Task<bool> UpdateJobApplication(JobApplication jobApplication);
        public Task<List<long>> GetJobApplicationStatsFunnelGraph(int lengthOfTime, string userAccountId);
        public Task<JobApplication> CheckJobApplicationAlreadyExists(string url);
    }
}
