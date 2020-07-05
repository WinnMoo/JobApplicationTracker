using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class JobPostingRepository
    {
        MongoClient db;
        private IMongoCollection<JobPosting> _jobPostings;
        public JobPostingRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            _jobPostings = database.GetCollection<JobPosting>("JobPostings");
        }

        public bool InsertJobPosting(JobPosting jobPosting)
        {
            bool inserted = false;
            try
            {
                _jobPostings.InsertOne(jobPosting);
                inserted = true;
                return inserted;
            } catch
            {
                return inserted;
            }
        }

        public JobPosting GetJobPosting(string url)
        {
            JobPosting posting = null;
            var filter = new BsonDocument("URL", url);
            try
            {
                posting = _jobPostings.Find(filter).FirstOrDefault();
                return posting;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<JobPosting> GetJobPostings()
        {
            throw new NotImplementedException();
        }

        public bool DeleteJobPosting(string url)
        {
            throw new NotImplementedException();
        }

        public bool UpdateJobPosting(JobPosting jobPosting)
        {
            bool updated = false;
            var filter = new BsonDocument("URL", jobPosting.URL);
            try
            {
                _jobPostings.ReplaceOne(filter, jobPosting);
                updated = true;
                return updated;
            } catch
            {
                return updated;
            }
        }
    }
}
