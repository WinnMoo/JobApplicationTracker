using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ManagerLayer.Managers;
using ManagerLayer.Requests;

namespace ControllerLayer.Controllers
{
    public class JobAppController : Controller
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        [HttpGet]
        [Route("api/jobapp/get")]
        public ActionResult GetJobApplications([FromBody] GetJobApplicationsRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.GetJobApplications(request);
        }

        [HttpPost]
        [Route("api/jobapp/add")]
        public ActionResult AddJobApplication([FromBody] JobApplicationRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("api/jobapp/delete")]
        public ActionResult DeleteJobApplication([FromBody] string jobApplicationId)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.DeleteJobApplication(jobApplicationId);
        }

        [HttpPut]
        [Route("api/jobapp/update")]
        public ActionResult UpdateJobApplication([FromBody] JobApplicationRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.UpdateJobApplication(request);
        }
        
    }
}
