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
        private readonly JobApplicationService jobAppService;
        private readonly UserAccountService userAccountService;
        public JobApplicationManager(MongoClient dbClient)
        {
            jobAppService = new JobApplicationService(dbClient);
            userAccountService = new UserAccountService(dbClient);
        }

        public ActionResult AddJobApplication(JobApplicationRequest request)
        {
            try
            {
                JobApplication newJobApp = new JobApplication(request.CompanyName, request.JobTitle, 
                                                              request.Description, request.Status, request.City, 
                                                              request.State, request.URLToJobPosting);
                var user = userAccountService.ReadUserFromDBUsingEmail(request.UserEmail.ToLower());
                newJobApp.UserAccountId = user.UserAccountId;
                if (!jobAppService.InsertJobApplication(newJobApp))
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    return new OkObjectResult(newJobApp);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        public ActionResult DeleteJobApplication(string jobApplicationId)
        {
            try
            {
                var jobApplication = jobAppService.GetJobApplication(jobApplicationId);
                if (jobApplication == null)
                {
                    return new NotFoundObjectResult("Job application not found");
                }
                if (!jobAppService.DeleteJobApplication(jobApplicationId))
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

                jobApplication.CompanyName = request.CompanyName;
                jobApplication.JobTitle = request.JobTitle;
                jobApplication.Description = request.Description;
                jobApplication.Status = request.Status;
                jobApplication.City = request.City;
                jobApplication.State = request.State;
                jobApplication.JobPostingURL = request.URLToJobPosting;

                if (!jobAppService.UpdatejobApplication(jobApplication))
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                } else
                {
                    return new OkObjectResult(jobApplication);
                }
            } catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public ActionResult GetJobApplications(int startIndex, int numOfItemsToGet, string emailAddress)
        {
            try
            {
                var user = userAccountService.ReadUserFromDBUsingEmail(emailAddress.ToLower());
                if(user == null)
                {
                    return new NotFoundObjectResult("Email address not found");
                }
                var jobApplications = jobAppService.GetJobApplications(user.UserAccountId, startIndex, numOfItemsToGet);
                return new OkObjectResult(jobApplications);
            } catch {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
