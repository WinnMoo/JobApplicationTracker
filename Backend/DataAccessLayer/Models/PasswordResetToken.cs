using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace DataAccessLayer.Models
{
    public class PasswordResetToken
    {
        public PasswordResetToken(string resetToken, string userId)
        {
            this.Token = resetToken;
            this.DateCreated = DateTime.UtcNow;
            this.Attempts = 0;
            this.UserId = userId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PasswordResetTokenId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public int Attempts { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool IsValid { get; set; }

    }
}
