﻿using System;
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
        public ActionResult GetJobApplications([FromQuery] int startIndex, [FromQuery] int numOfItemsToGet, [FromQuery] string emailAddress)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.GetJobApplications(startIndex, numOfItemsToGet, emailAddress);
        }

        [HttpPost]
        [Route("api/jobapp/add")]
        public ActionResult AddJobApplication([FromBody] JobApplicationRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.AddJobApplication(request);
        }

        [HttpDelete]
        [Route("api/jobapp/delete")]
        public ActionResult DeleteJobApplication([FromBody] DeleteJobApplicationRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.DeleteJobApplication(request.JobApplicationId);
        }

        [HttpPut]
        [Route("api/jobapp/update")]
        public ActionResult UpdateJobApplication([FromBody] JobApplicationRequest request)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.UpdateJobApplication(request);
        }
        
        [HttpGet]
        [Route("api/jobapp/getfunnelgraphstats")]
        public ActionResult GetFunnelGraphStats([FromQuery] int lengthOfTime, [FromQuery] string emailAddress)
        {
            JobApplicationManager _jobAppManager = new JobApplicationManager(new MongoClient(MONGODB_CONNECTION_STRING));
            return _jobAppManager.GetJobApplicationStatistics(lengthOfTime, emailAddress);
        }
    }
}
