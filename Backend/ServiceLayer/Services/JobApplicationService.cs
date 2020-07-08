﻿using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class JobApplicationService
    {
        private JobApplicationRepository _jobApplicationRepo;
        public JobApplicationService(MongoClient _db)
        {
            _jobApplicationRepo = new JobApplicationRepository(_db);
        }

        public bool InsertJobApplication(JobApplication jobApplication)
        {
            return _jobApplicationRepo.InsertJobApplication(jobApplication).Result;
        }

        public bool DeleteJobApplication(ObjectId jobApplicationId)
        {
            return _jobApplicationRepo.DeleteJobApplication(jobApplicationId).Result;
        }

        public bool UpdatejobApplication(JobApplication jobApplication)
        {
            return _jobApplicationRepo.UpdateJobApplication(jobApplication).Result;
        }

        public JobApplication GetJobApplication(ObjectId jobApplicationId)
        {
            return _jobApplicationRepo.GetJobApplication(jobApplicationId).Result; 
        }

        //TODO: add logic to get user specified number of job applications from db
        public List<JobApplication> GetJobApplications(ObjectId userId, int numberOfItems)
        {
            return _jobApplicationRepo.GetJobApplications(userId).Result;
        }

    }
}
