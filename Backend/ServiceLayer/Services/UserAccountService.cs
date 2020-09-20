using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using MongoDB.Bson;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace ServiceLayer.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userRepo;
        public UserAccountService(MongoClient _db)
        {
            _userRepo = new UserAccountRepository(_db);
        }
        public bool DeleteUserFromDB(string userId)
        {
            return _userRepo.DeleteUserAccount(userId).Result;
        }

        public bool InsertUserIntoDB(UserAccount userAccount)
        {
            return _userRepo.InsertUserAccountAsync(userAccount).Result;
        }

        public UserAccount ReadUserFromDBUsingId(string userId)
        {
            return _userRepo.GetUserAccountUsingId(userId).Result;
        }

        public UserAccount ReadUserFromDBUsingEmail(string emailAddress)
        {
            return _userRepo.GetUserAccountUsingEmail(emailAddress).Result;
        }

        public bool UpdateUserInDB(UserAccount userAccount)
        {
            return _userRepo.UpdateUserAccount(userAccount).Result;
        }
    }
}
