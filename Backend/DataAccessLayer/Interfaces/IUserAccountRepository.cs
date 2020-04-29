﻿using DataAccessLayer.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    interface IUserAccountRepository
    {
        public bool InsertUserAccount(UserAccount userAccount);
        public UserAccount GetUserAccount(ObjectId userId);
        public bool UpdateUserAccount(UserAccount userAccount);
        public bool DeleteUserAccount(ObjectId userId);
    }
}
