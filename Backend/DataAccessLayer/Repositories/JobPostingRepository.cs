using DataAccessLayer.Models;
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
            var _jobPostings = database.GetCollection<JobPosting>("jobPostings");
        }

        public bool InsertJobPosting(JobPosting jobPosting)
        {
            throw new NotImplementedException();
        }

        public JobPosting GetJobPosting(string url)
        {
            throw new NotImplementedException();
        }

        public List<JobPosting> GetJobPostings()
        {
            throw new NotImplementedException();
        }

        public bool DeleteJobPosting(string url)
        {
            throw new NotImplementedException();
        }

        public bool UpdateJobPosting(string url)
        {
            throw new NotImplementedException();
        }
    }
}
