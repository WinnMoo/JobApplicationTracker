using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class JobPostingService
    {
        private readonly JobPostingRepository _jobPostingRepo;
        public JobPostingService(MongoClient _db)
        {
            _jobPostingRepo = new JobPostingRepository(_db);
        }

        public bool InsertJobPosting(JobPosting jobPosting)
        {
            return _jobPostingRepo.InsertJobPosting(jobPosting).Result;
        }

        public bool DeleteJobPosting(string url)
        {
            return _jobPostingRepo.DeleteJobPosting(url);
        }

        public bool UpdateJobPosting(JobPosting jobPosting)
        {
            return _jobPostingRepo.UpdateJobPosting(jobPosting).Result;
        }

        public JobPosting GetJobPosting(string url)
        {
            return _jobPostingRepo.GetJobPosting(url).Result;
        }
    }
}
