using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.Models
{
    public class PasswordResetToken
    {
        public PasswordResetToken(string resetToken, ObjectId userId)
        {
            this.Token = resetToken;
            this.DateCreated = DateTime.Now;
            this.Attempts = 0;
            this.UserId = userId;
        }

        [BsonId]
        public ObjectId PasswordResetTokenId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public int Attempts { get; set; }
        [Required]
        public ObjectId UserId { get; set; }

    }
}
