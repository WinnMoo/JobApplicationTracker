using DataAccessLayer.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    interface IJobApplicationRepository
    {
        public bool InsertJobApplication(JobApplication jobApplication);
        public JobApplication GetJobApplication(ObjectId jobApplicationId);
        public List<JobApplication> GetJobApplications(ObjectId jobApplicationId);
        public bool DeleteJobApplication(ObjectId jobApplicationId);
        public bool UpdateJobApplication(JobApplication jobApplication);
    }
}
