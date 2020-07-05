using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ManagerLayer.Managers;
using ManagerLayer.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerLayer.Controllers
{
    public class JobAppController : Controller
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        [HttpGet]
        [Route("api/jobapp/get")]
        public ActionResult GetJobApplications([FromBody] JobApplicationRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("api/jobapp/add")]
        public ActionResult AddJobApplication([FromBody] JobApplicationRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("api/jobapp/delete")]
        public ActionResult DeleteJobApplication([FromBody] JobApplicationRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("api/jobapp/update")]
        public ActionResult UpdateJobApplication([FromBody] JobApplicationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
