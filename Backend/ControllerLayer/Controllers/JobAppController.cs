using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Requests;
using MongoDB.Driver;
using ManagerLayer.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerLayer.Controllers
{
    public class JobAppController : Controller
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);
        private MongoClient dbClient;
        private JobApplicationManager _jobAppManager;

        public JobAppController()
        {
            dbClient = new MongoClient(MONGODB_CONNECTION_STRING);
            _jobAppManager = new JobApplicationManager(dbClient);
        }
        [HttpGet]
        [Route("api/jobapp/get")]
        public ActionResult GetJobApplications([FromBody] JobApplicationRequest request)
        {
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
