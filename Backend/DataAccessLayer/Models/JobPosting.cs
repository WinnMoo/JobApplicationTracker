using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string city { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        
        // Counter to keep track of how many times this job posting has been added to a user's list of job applications
        [Required]
        public int AddedCounter { get; set; }
        [Required]
        public string URL { get; set; }
    }
}
