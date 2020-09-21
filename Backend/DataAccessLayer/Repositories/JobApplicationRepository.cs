using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly MongoClient db;
        private readonly IMongoCollection<JobApplication> _jobApplications;
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

        public async Task<JobApplication> GetJobApplicationUsingId(string jobApplicationId)
        {
            JobApplication retrievedJobApplication = null;
            
            var filter = new BsonDocument("JobApplicationId", jobApplicationId);
            try
            {
                retrievedJobApplication = await _jobApplications.Find(x => x.JobApplicationId == jobApplicationId).FirstOrDefaultAsync();
                return retrievedJobApplication;
            }
            catch
            {
                return retrievedJobApplication;
            }
        }
        public async Task<JobApplication> GetJobApplicationUsingUrl(string jobPostingURL)
        {
            JobApplication retrievedJobApplication = null;

            var filter = new BsonDocument("JobPostingURL", jobPostingURL);
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
        /// <summary>
        /// Method to get JobApplications from data store
        /// </summary>
        /// <param name="userId"> Id associated with user</param>
        /// <param name="startIndex">The index of where in the list to start, used to get applications from specific index</param>
        /// <param name="numberOfItems">The number of job applications to get from the data store</param>
        /// <returns>List of job applications</returns>
        public async Task<List<JobApplication>> GetJobApplications(string userId, int startIndex, int numberOfItems )
        {
            var JobApplications = new List<JobApplication>();
            var filter = new BsonDocument("UserAccountId", userId);
            try
            {
                var index = startIndex + numberOfItems;
                if(index == -1) // If numberOfItems == -1, return all items, otherwise only return a window of items
                {
                    await _jobApplications.Find(filter).ToListAsync();
                } else {
                    await _jobApplications.Find(filter).Skip(startIndex).Limit(numberOfItems).ToListAsync();
                }
            }
            catch
            {
                return null;
            }
            return JobApplications;
        }

        public async Task<bool> DeleteJobApplication(string jobApplicationId)
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
            try
            {
                await _jobApplications.ReplaceOneAsync(x => x.JobApplicationId == updatedJobApplication.JobApplicationId, updatedJobApplication);
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
