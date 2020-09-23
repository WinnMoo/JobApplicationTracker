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

        public async Task<bool> DeleteUserAccount(string userId)
        {
            var deleted = false;
            try
            {
                await _userAccounts.DeleteOneAsync(x => x.UserAccountId == userId);
                deleted = true;
            }
            catch
            {
                return deleted;
            }
            return deleted;
        }

        public async Task<UserAccount> GetUserAccountUsingId(string userId)
        {
            UserAccount retrievedUserAccount = null;
            try
            {
                retrievedUserAccount = await _userAccounts.Find(x => x.UserAccountId == userId).FirstOrDefaultAsync();
            }
            catch
            {
                return retrievedUserAccount;
            }
            return retrievedUserAccount;
        }

        public async Task<UserAccount> GetUserAccountUsingEmail(string emailAddress)
        {
            UserAccount retrievedUserAccount = null;
            try
            {
                retrievedUserAccount =  await _userAccounts.Find(x => x.Email == emailAddress).FirstOrDefaultAsync();
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
            try
            {
                await _userAccounts.ReplaceOneAsync(x => x.UserAccountId == updatedUserAccount.UserAccountId, updatedUserAccount);
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
