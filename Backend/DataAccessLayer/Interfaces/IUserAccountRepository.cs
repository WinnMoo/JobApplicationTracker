using DataAccessLayer.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IUserAccountRepository
    {
        public bool InsertUserAccount(UserAccount userAccount);
        public UserAccount GetUserAccount(ObjectId userId);
        public UserAccount GetUserAccount(string emailAddress);
        public bool UpdateUserAccount(UserAccount userAccount);
        public bool DeleteUserAccount(ObjectId userId);
    }
}
