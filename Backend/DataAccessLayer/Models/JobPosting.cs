using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        public JobPosting(string jobTitle, string company, string url)
        {
            this.JobTitle = jobTitle;
            this.Company = company;
            this.AddedCounter = 0;
            this.URL = url;
            this.IsActive = true;
            this.DateAdded = DateTime.Now;
        }
        public JobPosting(string jobTitle, string company, string state, string url)
        {
            this.JobTitle = jobTitle;
            this.Company = company;
            this.State = state;
            this.AddedCounter = 0;
            this.URL = url;
            this.IsActive = true;
            this.DateAdded = DateTime.Now;
        }
        public JobPosting(string jobTitle, string company, string city, string state, string url)
        {
            this.JobTitle = jobTitle;
            this.Company = company;
            this.City = city;
            this.State = state;
            this.AddedCounter = 0;
            this.URL = url;
            this.IsActive = true;
            this.DateAdded = DateTime.Now;
        }
        public JobPosting(string jobTitle, string company, string city, string state, string zipCode, string url)
        {
            this.JobTitle = jobTitle;
            this.Company = company;
            this.State = state;
            this.City = city;
            this.ZipCode = zipCode;
            this.AddedCounter = 0;
            this.URL = url;
            this.IsActive = true;
            this.DateAdded = DateTime.Now;
        }
    }
}
