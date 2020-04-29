using DataAccessLayer.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Interfaces
{
    public interface IUserAccountService
    {
        public bool InsertUserIntoDB(UserAccount userAccount);
        public bool DeleteUserFromDB(ObjectId userId);
        public bool UpdateUserInDB(UserAccount userAccount);
        public UserAccount ReadUserFromDB(ObjectId userId);
    }
}
