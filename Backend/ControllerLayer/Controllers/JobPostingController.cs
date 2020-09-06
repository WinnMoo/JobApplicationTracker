using System;
using ManagerLayer.Managers;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ControllerLayer.Controllers
{
    public class JobPostingController : ControllerBase
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        [HttpPost]
        [Route("api/jobposting/add")]
        public ActionResult AddJobPosting([FromBody] JobPostingRequest request)
        {
            try
            {
                JobPostingManager _jobPostingManager = new JobPostingManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _jobPostingManager.AddJobPosting(request.URL);
            }
            catch(Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/jobposting/purge")]
        public ActionResult PurgeJobPostings()
        {
            try
            {
                JobPostingManager _jobPostingManager = new JobPostingManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _jobPostingManager.ValidateJobPostings();
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/jobposting/parse")]
        public ActionResult ParseJobPosting([FromBody] string urlToParse)
        {
            try
            {
                JobPostingManager _jobPostingManager = new JobPostingManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return 
            }
        }
    }
}
