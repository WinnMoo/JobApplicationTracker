using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer.Services;
using System;

namespace ManagerLayer.Managers
{
    public class JobPostingManager
    {
        private readonly JobPostingService _jobPostingService;
        private readonly JobApplicationService _jobAppService;
        public JobPostingManager(MongoClient _db)
        {
            _jobPostingService = new JobPostingService(_db);
            _jobAppService = new JobApplicationService(_db);
        }

        /// <summary>
        /// Method to add a job posting 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddJobPosting(string url)
        {
            var retrievedJobApp = _jobAppService.CheckJobApplicationExists(url);
            if(retrievedJobApp != null)
            {
                return new ConflictObjectResult("You've already added this job posting");
            }
            var JobPostToAdd = ParseJobPosting(url);
            if(JobPostToAdd == null)
            {
                return new BadRequestObjectResult("Unable to parse the given URL");
            }
            if (InsertJobPostingToDB(JobPostToAdd))
            {
                return new ObjectResult(JobPostToAdd) { StatusCode = StatusCodes.Status201Created };
            }
            else
            {
                return new OkObjectResult(JobPostToAdd);
            }
        }


        /// <summary>
        /// Parses the given URL for information
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private JobPosting ParseJobPosting(string url)
        {
            JobPostingParserFactoryService _parserFactoryService = new JobPostingParserFactoryService();
            JobPosting _jobPosting;
            var parser = _parserFactoryService.getParser(url); // Selects the proper scraper per the URL
            if (parser == null) 
            {
                return null;
            }
            
            _jobPosting = parser.ScrapeAndReturnPostingInfo(url);

            return _jobPosting;
        }

        /// <summary>
        /// Method to insert job posting to DB
        /// </summary>
        /// <param name="jobPosting"></param>
        private bool InsertJobPostingToDB(JobPosting jobPosting)
        {
            var exists = _jobPostingService.GetJobPosting(jobPosting.URL);
            if(exists == null)
            {
                return _jobPostingService.InsertJobPosting(jobPosting);
            }
            return false;
        }

        /// <summary>
        /// Method to void job postings that are over 30 days old.
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateJobPostings()
        {
            throw new NotImplementedException();
        }
    }
}
