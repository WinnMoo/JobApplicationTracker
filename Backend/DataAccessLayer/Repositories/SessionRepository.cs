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
    public class SessionRepository : ISessionRepository
    {
        private MongoClient db;
        private IMongoCollection<Session> _sessions;

        public SessionRepository(MongoClient _db)
        {
            this.db = _db;
            var database = this.db.GetDatabase("Database");
            _sessions = database.GetCollection<Session>("Sessions");
        }

        public async Task<bool> AddSession(Session session)
        {
            bool Added = false;
            try
            {
                await _sessions.InsertOneAsync(session);
                Added = true;
            } catch (Exception e)
            {
                return Added;
            }
            return Added;
        }

        public async Task<bool> DeleteSession(Session session)
        {
            bool Deleted = false;
            var filter = new BsonDocument("SessionId", session.SessionId);
            try
            {
                await _sessions.DeleteOneAsync(filter);
                Deleted = true;
            }
            catch
            {
                return Deleted;
            }
            return Deleted;
        }

        public async Task<Session> GetSession(string emailAddress)
        {
            var filter = new BsonDocument("EmailAddress", emailAddress);
            try
            {
                return await _sessions.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateSession(Session session)
        {
            bool updated = false;
            var filter = new BsonDocument("SessionId", session.SessionId);
            try
            {
                await _sessions.ReplaceOneAsync(filter, session);
                updated = true;
            } catch(Exception e)
            {
                return updated;
            }
            return updated;
        }

        public void ExpireAllSessions(string jwtToken)
        {
            throw new NotImplementedException();
        }
    
    }
}
