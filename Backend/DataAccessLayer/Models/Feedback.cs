using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FeedbackId { get; set; }
        [BsonRequired]
        public int Category { get; set; }
        [BsonRequired]
        public int Rating { get; set; }
        [BsonRequired]
        public string UserFeedback { get; set; }
        [BsonRequired]
        public DateTime DateSubmitted { get; set; }

        public Feedback(int category, int rating, string userFeedback)
        {
            this.FeedbackId = ObjectId.GenerateNewId().ToString();
            this.Category = category;
            this.Rating = rating;
            this.UserFeedback = userFeedback;
            this.DateSubmitted = DateTime.UtcNow;
        }
    }
}
