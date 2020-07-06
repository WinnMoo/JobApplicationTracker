using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Console.WriteLine(e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
