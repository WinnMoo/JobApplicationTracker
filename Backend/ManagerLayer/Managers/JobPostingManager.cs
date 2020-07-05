using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Method to add a job posting 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddJobPosting(string url)
        {
            var JobPostToAdd = ParseJobPosting(url);
            if(JobPostToAdd == null)
            {
                return new BadRequestObjectResult("Unable to parse the given URL");
            }
            if (InsertJobPostingToDB(JobPostToAdd))
            {
                return new OkObjectResult("Job Posting has been successfully added.");
            } 
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Parses the given URL for information
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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

            return _jobPosting;
        }

        /// <summary>
        /// Method to insert job posting to DB
        /// </summary>
        /// <param name="jobPosting"></param>
        public bool InsertJobPostingToDB(JobPosting jobPosting)
        {
            var exists = _jobPostingService.GetJobPosting(jobPosting.URL);
            if(exists != null)
            {
                _jobPostingService.InsertJobPosting(jobPosting);
                return true;
            }
            return false;
        }
    }
}
