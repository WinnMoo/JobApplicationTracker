using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataAccessLayer.Models
{
    public class JobPosting
    {
        [BsonId]
        public ObjectId JobPostingId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Description { get; set; }
        public double Salary { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        
        // Counter to keep track of how many times this job posting has been added to a user's list of job applications
        [Required]
        public int AddedCounter { get; set; }
        [Required]
        public string URL { get; set; }
        public JobPosting(string jobTitle, string company, string location, string url)
        {
            this.JobTitle = jobTitle;
            this.Company = company;
            this.City = location;
            this.AddedCounter = 0;
            this.URL = url;
        }
    }
}
