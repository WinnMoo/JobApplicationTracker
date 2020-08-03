using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ResetTokenRepository
    {
        MongoClient db;
        private IMongoCollection<PasswordResetToken> _resetTokens;

        public ResetTokenRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            _resetTokens = database.GetCollection<PasswordResetToken>("PasswordResetTokens");
        }

        public async Task<PasswordResetToken> GetToken(string resetToken)
        {
            PasswordResetToken token = null;
            var filter = new BsonDocument("Token", resetToken);
            try
            {
                token = await _resetTokens.Find(filter).FirstOrDefaultAsync();
                return token;
            }
            catch
            {
                return null;
            }
        }

        public ICollection<PasswordResetToken> GetTokens(ObjectId userId)
        {
            var filter = new BsonDocument("UserId", userId);
            try
            {
                var tokens = _resetTokens.AsQueryable<PasswordResetToken>().Where(r => r.UserId == userId).ToList<PasswordResetToken>();
                return tokens;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> InsertToken(PasswordResetToken token)
        {
            bool inserted = false;
            try
            {
                await _resetTokens.InsertOneAsync(token);
                inserted = true;
                return inserted;
            } catch
            {
                return inserted;
            }
        }

        public async Task<bool> UpdateToken(PasswordResetToken token)
        {
            bool updated = false;
            var filter = new BsonDocument("PasswordResetTokenId", token.PasswordResetTokenId);
            try
            {
                await _resetTokens.ReplaceOneAsync(filter, token);
                updated = true;
            } catch
            {
                return updated;
            }
            return updated;
        }

        public async Task<bool> DeleteToken(string token)
        {
            bool deleted = false;
            var filter = new BsonDocument("Token", token);
            try
            {
                await _resetTokens.DeleteOneAsync(filter);
                deleted = true;
            } catch
            {
                return deleted;
            }
            return deleted;
        }
    }
}
