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
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserAccountId { get; set; }
        [BsonRequired]
        public string FirstName {get; set;}
        [BsonRequired]
        public string Email { get; set; }
        [BsonRequired]
        public string PasswordHash { get; set; }
        [BsonRequired]
        public byte[] PasswordSalt { get; set; }
        [BsonRequired]
        public string SecurityQuestion1 { get; set; }
        [BsonRequired]
        public string SecurityQuestion2 { get; set; }
        [BsonRequired]
        public string SecurityQuestion3 { get; set; }
        [BsonRequired]
        public string SecurityAnswer1 { get; set; }
        [BsonRequired]
        public string SecurityAnswer2 { get; set; }
        [BsonRequired]
        public string SecurityAnswer3 { get; set; }

    }
}
