using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class JobApplication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string JobApplicationId { get; set; }
        [BsonRequired]
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string JobPostingURL { get; set; }
        [BsonRequired]
        public DateTime DateApplied { get; set; }
        public string UserAccountId { get; set; }
        public JobApplication()
        {

        }

        public JobApplication(string companyName, string jobTitle, string description, int status, string city, string state, string link)
        {
            this.JobApplicationId = ObjectId.GenerateNewId().ToString();
            this.CompanyName = companyName;
            this.JobTitle = jobTitle;
            this.Description = description;
            this.Status = status;
            this.DateApplied = DateTime.UtcNow;
            this.City = city;
            this.State = state;
            this.JobPostingURL = link;
        }
    }
}
