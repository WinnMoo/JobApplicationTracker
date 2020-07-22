using DataAccessLayer.Models;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer.Services;
using System;

namespace ManagerLayer.Managers
{
    public class JobApplicationManager
    {
        private JobApplicationService jobAppService;
        private UserAccountService userAccountService;
        public JobApplicationManager(MongoClient dbClient)
        {
            jobAppService = new JobApplicationService(dbClient);
            userAccountService = new UserAccountService(dbClient);
        }

        public ActionResult AddJobApplication(JobApplicationRequest request)
        {
            try
            {
                JobApplication newJobApp = new JobApplication(request.CompanyName, request.Description, request.City, request.State, request.URLToJobPosting, request.UserFields);
                var user = userAccountService.ReadUserFromDB(request.UserEmail);
                newJobApp.UserAccountId = user.UserAccountId;
                if (!jobAppService.InsertJobApplication(newJobApp))
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    return new OkObjectResult("Added job application successfully");
                }
            } catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        public ActionResult DeleteJobApplication(JobApplicationRequest request)
        {
            try
            {
                var jobApplication = jobAppService.GetJobApplication(request.JobAppId);
                if (jobApplication == null)
                {
                    return new BadRequestObjectResult("Invalid job application");
                }
                if (!jobAppService.DeleteJobApplication(jobApplication.JobApplicationId))
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                } else
                {
                    return new OkObjectResult("Deleted job application successfully");
                }
            } catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public ActionResult UpdateJobApplication(JobApplicationRequest request)
        {
            try
            {
                var jobApplication = jobAppService.GetJobApplication(request.JobAppId);
                if (jobApplication == null)
                {
                    return new BadRequestObjectResult("Invalid job application");
                }

                jobApplication.City = request.City;
                jobApplication.CompanyName = request.CompanyName;
                jobApplication.JobPostingURL = request.URLToJobPosting;
                jobApplication.ZipCode = request.ZipCode;
                jobApplication.UserDefinedFields = request.UserFields;
                jobApplication.Description = request.Description;
                jobApplication.JobTitle = request.JobTitle;
                jobApplication.State = request.State;

                if (!jobAppService.UpdatejobApplication(jobApplication))
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                } else
                {
                    return new OkObjectResult("Deleted job application successfully");
                }
            } catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public ActionResult GetJobApplications(GetJobApplicationsRequest request)
        {
            try
            {
                var user = userAccountService.ReadUserFromDB(request.EmailAddress);
                jobAppService.GetJobApplications(user.UserAccountId, request.StartIndex, request.NumOfItemsToGet);


                throw new NotImplementedException();
            } catch {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
