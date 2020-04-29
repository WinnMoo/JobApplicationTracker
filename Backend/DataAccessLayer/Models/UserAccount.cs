using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class UserAccount
    {
        [BsonId]
        public ObjectId UserAccountId { get; set; }
        [BsonRequired]
        public string FirstName {get; set;}
        [BsonRequired]
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
