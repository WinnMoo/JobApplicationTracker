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

namespace DataAccessLayer.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        MongoClient db;
        private IMongoCollection<JobApplication> _jobApplications;
        public JobApplicationRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            var _jobApplications = database.GetCollection<JobApplication>("JobApplications");
        }

        public bool InsertJobApplication(JobApplication jobApplication)
        {
            bool inserted = false;
            try
            {
                _jobApplications.InsertOneAsync(jobApplication);
                inserted = true;
            }
            catch
            {
                return inserted;
            }
            return inserted;
        }

        public JobApplication GetJobApplication(ObjectId jobApplicationId)
        {
            JobApplication retrievedJobApplication = null;
            
            var filter = new BsonDocument("JobApplicationId", jobApplicationId);
            try
            {
                retrievedJobApplication = _jobApplications.Find(filter).FirstOrDefault();
                return retrievedJobApplication;
            }
            catch
            {
                return retrievedJobApplication;
            }
        }

        public List<JobApplication> GetJobApplications(ObjectId userId)
        {
            var JobApplications = new List<JobApplication>();
            var filter = new BsonDocument("UserAccountId", userId);
            try
            {
                _jobApplications.Find(filter).ForEachAsync(document => JobApplications.Add(document));
            }
            catch
            {
                return null;
            }
            return JobApplications;
        }

        public bool DeleteJobApplication(ObjectId jobApplicationId)
        {
            var deleted = false;
            var deleteFilter = new BsonDocument("JobApplicationId", jobApplicationId);
            try
            {
                _jobApplications.DeleteOneAsync(deleteFilter);
                deleted = true;
            }
            catch
            {
                return deleted;
            }
            return deleted;
        }

        public bool UpdateJobApplication(JobApplication updatedJobApplication)
        {
            var updated = false;
            var updateFilter = new BsonDocument("JobApplicationId", updatedJobApplication.JobApplicationId);
            try
            {
                _jobApplications.ReplaceOneAsync(updateFilter, updatedJobApplication);
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
