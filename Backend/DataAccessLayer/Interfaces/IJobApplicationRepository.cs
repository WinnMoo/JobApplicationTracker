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
        public Task<JobApplication> GetJobApplication(ObjectId jobApplicationId);
        public Task<List<JobApplication>> GetJobApplications(ObjectId jobApplicationId);
        public Task<bool> DeleteJobApplication(ObjectId jobApplicationId);
        public Task<bool> UpdateJobApplication(JobApplication jobApplication);
    }
}
