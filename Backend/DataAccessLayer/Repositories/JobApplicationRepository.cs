using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private MongoClient db;
        private IMongoCollection<JobApplication> _jobApplications;
        public JobApplicationRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            _jobApplications = database.GetCollection<JobApplication>("JobApplications");
        }

        public async Task<bool> InsertJobApplication(JobApplication jobApplication)
        {
            bool inserted = false;
            try
            {
                await _jobApplications.InsertOneAsync(jobApplication);
                inserted = true;
            }
            catch
            {
                return inserted;
            }
            return inserted;
        }

        public async Task<JobApplication> GetJobApplication(ObjectId jobApplicationId)
        {
            JobApplication retrievedJobApplication = null;
            
            var filter = new BsonDocument("JobApplicationId", jobApplicationId);
            try
            {
                retrievedJobApplication = await _jobApplications.Find(filter).FirstOrDefaultAsync();
                return retrievedJobApplication;
            }
            catch
            {
                return retrievedJobApplication;
            }
        }

        public async Task<List<JobApplication>> GetJobApplications(ObjectId userId)
        {
            var JobApplications = new List<JobApplication>();
            var filter = new BsonDocument("UserAccountId", userId);
            try
            {
                await _jobApplications.Find(filter).ForEachAsync(document => JobApplications.Add(document));
            }
            catch
            {
                return null;
            }
            return JobApplications;
        }

        public async Task<bool> DeleteJobApplication(ObjectId jobApplicationId)
        {
            var deleted = false;
            var deleteFilter = new BsonDocument("JobApplicationId", jobApplicationId);
            try
            {
                await _jobApplications.DeleteOneAsync(deleteFilter);
                deleted = true;
            }
            catch
            {
                return deleted;
            }
            return deleted;
        }

        public async Task<bool> UpdateJobApplication(JobApplication updatedJobApplication)
        {
            var updated = false;
            var updateFilter = new BsonDocument("JobApplicationId", updatedJobApplication.JobApplicationId);
            try
            {
                await _jobApplications.ReplaceOneAsync(updateFilter, updatedJobApplication);
                updated = true;
            }
            catch
            {
                return updated;
            }
            return updated;
        }
        
    }
}
