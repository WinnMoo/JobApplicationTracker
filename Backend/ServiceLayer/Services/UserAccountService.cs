﻿using DataAccessLayer.Models;
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
        private IUserAccountRepository _userRepo;
        public UserAccountService(IMongoDatabase db)
        {
            _userRepo = new UserAccountRepository(db);
        }
        public bool DeleteUserFromDB(ObjectId userId)
        {
            return _userRepo.DeleteUserAccount(userId);
        }

        public bool InsertUserIntoDB(UserAccount userAccount)
        {
            return _userRepo.InsertUserAccount(userAccount);
        }

        public UserAccount ReadUserFromDB(ObjectId userId)
        {
            return _userRepo.GetUserAccount(userId);
        }

        public UserAccount ReadUserFromDB(string emailAddress)
        {
            return _userRepo.GetUserAccount(emailAddress);
        }

        public bool UpdateUserInDB(UserAccount userAccount)
        {
            return _userRepo.UpdateUserAccount(userAccount);
        }
    }
}
