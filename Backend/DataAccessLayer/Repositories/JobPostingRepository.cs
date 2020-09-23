using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> InsertJobPosting(JobPosting jobPosting)
        {
            bool inserted = false;
            try
            {
                await _jobPostings.InsertOneAsync(jobPosting);
                inserted = true;
                return inserted;
            } catch
            {
                return inserted;
            }
        }

        public async Task<JobPosting> GetJobPosting(string url)
        {
            JobPosting posting = null;
            try
            {
                posting = await _jobPostings.Find(x => x.URL == url).FirstOrDefaultAsync();
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

        public async Task<bool> UpdateJobPosting(JobPosting jobPosting)
        {
            bool updated = false;
            try
            {
                await _jobPostings.ReplaceOneAsync(x => x.URL == jobPosting.URL, jobPosting);
                updated = true;
                return updated;
            } catch
            {
                return updated;
            }
        }

        public void ValidateJobPostings()
        {
            var update = new BsonDocument("IsActive", false);
            _jobPostings.UpdateManyAsync(x => x.DateAdded.AddDays(30) <= DateTime.UtcNow, update);
        }
    }
}
