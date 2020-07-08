using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

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

        public PasswordResetToken GetToken(string resetToken)
        {
            PasswordResetToken token = null;
            var filter = new BsonDocument("Token", resetToken);
            try
            {
                token = _resetTokens.Find(filter).FirstOrDefault();
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

        public bool InsertToken(PasswordResetToken token)
        {
            bool inserted = false;
            try
            {
                _resetTokens.InsertOneAsync(token);
                inserted = true;
                return inserted;
            } catch
            {
                return inserted;
            }
        }

        public bool UpdateToken(PasswordResetToken token)
        {
            bool updated = false;
            var filter = new BsonDocument("PasswordResetTokenId", token.PasswordResetTokenId);
            try
            {
                _resetTokens.ReplaceOneAsync(filter, token);
                updated = true;
            } catch
            {
                return updated;
            }
            return updated;
        }
    }
}
