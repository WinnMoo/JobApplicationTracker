using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class JobApplicationRequest
    {
        public ObjectId JobAppId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Description { get; set; }
        public int Status { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string URLToJobPosting { get; set; }
        public DateTime DateApplied { get; set; }
        public Dictionary<string, string> UserFields { get; set; }
        public string UserEmail { get; set; }
    }
}
