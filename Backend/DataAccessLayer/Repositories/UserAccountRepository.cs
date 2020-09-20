using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        readonly MongoClient db;
        readonly private IMongoCollection<UserAccount> _userAccounts;
        public UserAccountRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            _userAccounts = database.GetCollection<UserAccount>("UserAccounts");
        }

        public async Task<bool> DeleteUserAccount(ObjectId userId)
        {
            var deleted = false;
            try
            {
                var filter = new BsonDocument("UserId", userId);
                await _userAccounts.DeleteOneAsync(filter);
                deleted = true;
            }
            catch
            {
                return deleted;
            }
            return deleted;
        }

        public async Task<UserAccount> GetUserAccount(ObjectId userId)
        {
            UserAccount retrievedUserAccount = null;
            var filter = new BsonDocument("UserId", userId);
            try
            {
                retrievedUserAccount = await _userAccounts.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                return retrievedUserAccount;
            }
            return retrievedUserAccount;
        }

        public async Task<UserAccount> GetUserAccount(string emailAddress)
        {
            UserAccount retrievedUserAccount = null;
            var filter = new BsonDocument("Email", emailAddress);
            try
            {
                retrievedUserAccount =  await _userAccounts.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                return retrievedUserAccount;
            }
            return retrievedUserAccount;
        }

        public async Task<bool> InsertUserAccountAsync(UserAccount userAccount)
        {
            bool inserted = false;
            try
            {
                await _userAccounts.InsertOneAsync(userAccount);
                inserted = true;
            }
            catch (Exception e)
            {
                return inserted;
            }
            return inserted;
        }

        public async Task<bool> UpdateUserAccount(UserAccount updatedUserAccount)
        {
            var updated = false;
            var updateFilter = new BsonDocument("UserAccountId", updatedUserAccount.UserAccountId);
            try
            {
                await _userAccounts.ReplaceOneAsync(updateFilter, updatedUserAccount);
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
