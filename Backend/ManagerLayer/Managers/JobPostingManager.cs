using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using MongoDB.Driver;
using ServiceLayer.Services;
using ServiceLayer.Services.JobParserServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Managers
{
    public class JobPostingManager
    {
        private JobPostingService _jobPostingService;
        public JobPostingManager(MongoClient _db)
        {
            _jobPostingService = new JobPostingService(_db);
        }

        public JobPosting ParseJobPosting(string url)
        {
            JobPostingParserFactoryService _parserFactoryService = new JobPostingParserFactoryService();
            JobPosting _jobPosting;
            var parser = _parserFactoryService.getParser(url); // Selects the proper scraper per the URL
            if (parser == null) 
            {
                return null;
            }
            
            _jobPosting = parser.ScrapeAndReturnPostingInfo(url);

            AddJobPosting(_jobPosting);

            return _jobPosting;
        }

        public void AddJobPosting(JobPosting jobPosting)
        {
            var exists = _jobPostingService.GetJobPosting(jobPosting.URL);
            if(exists != null)
            {
                _jobPostingService.InsertJobPosting(jobPosting);
            }
        }
    }
}
