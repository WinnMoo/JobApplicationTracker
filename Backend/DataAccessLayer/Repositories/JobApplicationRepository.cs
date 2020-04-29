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
        IMongoDatabase db;
        public JobApplicationRepository(IMongoDatabase _db)
        {
            this.db = _db;
        }

        public bool InsertJobApplication(JobApplication jobApplication)
        {
            bool inserted = false;
            try
            {
                var collection = db.GetCollection<JobApplication>("jobapplications");
                collection.InsertOneAsync(jobApplication);
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
                var collection = db.GetCollection<JobApplication>("jobapplications");
                retrievedJobApplication = collection.Find(filter).FirstOrDefault();
            }
            catch
            {
                
            }
            return retrievedJobApplication;
        }

        public List<JobApplication> GetJobApplications(ObjectId userId)
        {
            var JobApplications = new List<JobApplication>();
            var filter = new BsonDocument("UserAccountId", userId);
            try
            {
                var collection = db.GetCollection<JobApplication>("jobapplications");
                collection.Find(filter).ForEachAsync(document => JobApplications.Add(document));
            }
            catch
            {

            }
            return JobApplications;
        }

        public bool DeleteJobApplication(ObjectId jobApplicationId)
        {
            var deleted = false;
            
            var deleteFilter = new BsonDocument("JobApplicationId", jobApplicationId);
            try
            {
                var collection = db.GetCollection<JobApplication>("jobapplications");
                collection.DeleteOneAsync(deleteFilter);
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
                var collection = db.GetCollection<JobApplication>("jobapplications");
                collection.ReplaceOneAsync(updateFilter, updatedJobApplication);
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
