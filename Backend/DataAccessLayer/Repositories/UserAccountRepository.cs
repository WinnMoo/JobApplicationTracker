using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        MongoClient db;
        private IMongoCollection<UserAccount> _userAccounts;
        public UserAccountRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            _userAccounts = database.GetCollection<UserAccount>("UserAccount");
        }

        public bool DeleteUserAccount(ObjectId userId)
        {
            var deleted = false;
            try
            {
                var filter = new BsonDocument("UserId", userId);
                _userAccounts.DeleteOneAsync(filter);
            }
            catch
            {
                return deleted;
            }
            return deleted;
        }

        public UserAccount GetUserAccount(ObjectId userId)
        {
            UserAccount retrievedUserAccount = null;
            var filter = new BsonDocument("UserId", userId);
            try
            {
                retrievedUserAccount = _userAccounts.Find(filter).FirstOrDefault();
            }
            catch
            {
                return retrievedUserAccount;
            }
            return retrievedUserAccount;
        }

        public UserAccount GetUserAccount(string emailAddress)
        {
            UserAccount retrievedUserAccount = null;
            var filter = new BsonDocument("Email", emailAddress);
            try
            {
                retrievedUserAccount = _userAccounts.Find(filter).FirstOrDefault();

            }
            catch
            {
                return retrievedUserAccount;
            }
            return retrievedUserAccount;
        }

        public bool InsertUserAccount(UserAccount userAccount)
        {
            bool inserted = false;
            try
            {
                _userAccounts.InsertOne(userAccount);
                inserted = true;
            }
            catch (Exception e)
            {
                return inserted;
            }
            return inserted;
        }

        public bool UpdateUserAccount(UserAccount updatedUserAccount)
        {
            var updated = false;
            var updateFilter = new BsonDocument("UserAccountId", updatedUserAccount.UserAccountId);
            try
            {
                _userAccounts.ReplaceOne(updateFilter, updatedUserAccount);
                updated = true;
            }
            catch
            {
                return updated;
            }
            return updated;
        }
    }
}
