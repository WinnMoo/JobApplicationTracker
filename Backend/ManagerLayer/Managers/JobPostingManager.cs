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
        public JobPostingManager()
        {

        }
        

        public JobPosting ParseJobPosting(string url)
        {
            JobPostingParserFactoryService _parserFactoryService = new JobPostingParserFactoryService();
            JobPosting _jobPosting;
            var parser = _parserFactoryService.getParser(url);
            if(parser == null) // Selects the proper scraper per the URL
            {
                return null;
            }
            // TODO: Add the newly parsed job posting to the job posting repository
            _jobPosting = parser.ScrapeAndReturnPostingInfo(url);
            return _jobPosting;
        }
    }
}
