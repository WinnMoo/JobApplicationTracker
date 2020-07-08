﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Session
    {
        [Required]
        [BsonId]
        public ObjectId SessionId { get; set; }
        [Required]
        public string JWTToken { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime DateIssued { get; set; }
        [Required]
        public DateTime DateExpired { get; set; }

        public Session(string emailAddress, string jwtToken)
        {
            this.EmailAddress = emailAddress;
            this.JWTToken = jwtToken;
            this.DateIssued = DateTime.Now;
            this.DateExpired = DateTime.Now.AddMinutes(30);
        }
    }
}