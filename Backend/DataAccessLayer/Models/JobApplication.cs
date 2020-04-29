using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace DataAccessLayer.Models
{
    public class JobApplication
    {
        [BsonId]
        public ObjectId JobApplicationId { get; set; }
        [BsonRequired]
        public string CompanyName { get; set; }
        [BsonRequired]
        public DateTime DateApplied { get; set; }
        public string Location { get; set; }
        public string JobPostingLink { get; set; }
        public ObjectId UserAccountId { get; set; }
        public JobApplication()
        {

        }
        public JobApplication(string companyName, string location, string link)
        {
            this.JobApplicationId = ObjectId.GenerateNewId();
            this.CompanyName = companyName;
            this.DateApplied = DateTime.Today;
            this.Location = location;
            this.JobPostingLink = link;
        }
        
    }
}
