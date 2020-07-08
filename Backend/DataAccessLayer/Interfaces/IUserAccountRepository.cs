using DataAccessLayer.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserAccountRepository
    {
        public Task<bool> InsertUserAccountAsync(UserAccount userAccount);
        public Task<UserAccount> GetUserAccount(ObjectId userId);
        public Task<UserAccount> GetUserAccount(string emailAddress);
        public Task<bool> UpdateUserAccount(UserAccount userAccount);
        public Task<bool> DeleteUserAccount(ObjectId userId);
    }
}
