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
        IMongoDatabase db;
        private readonly IMongoCollection<UserAccount> _userAccounts;
        public UserAccountRepository(IMongoDatabase _db)
        {
            this.db = _db;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _userAccounts = database.GetCollection<UserAccount>(settings.UserAccountsCollectionName);
            
        }

        public bool DeleteUserAccount(ObjectId userId)
        {
            var deleted = false;
            try
            {
                var filter = new BsonDocument("UserId", userId);

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
                var collection = db.GetCollection<UserAccount>("useraccounts");
                retrievedUserAccount = collection.Find(filter).FirstOrDefault();

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
                _userAccounts.InsertOneAsync(userAccount);
                inserted = true;
            }
            catch
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
                var collection = db.GetCollection<UserAccount>("useraccounts");
                collection.ReplaceOneAsync(updateFilter, updatedUserAccount);
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
