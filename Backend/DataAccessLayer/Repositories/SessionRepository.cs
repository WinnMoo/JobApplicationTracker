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
        private readonly MongoClient db;
        private readonly IMongoCollection<Session> _sessions;

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
            try
            {
                await _sessions.DeleteOneAsync(x => x.SessionId == session.SessionId);
                Deleted = true;
            }
            catch
            {
                return Deleted;
            }
            return Deleted;
        }

        public async Task<Session> GetSession(string JWTToken)
        {
            try
            {
                return await _sessions.Find(x => x.JWTToken == JWTToken).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> UpdateSession(Session session)
        {
            bool updated = false;
            try
            {
                await _sessions.ReplaceOneAsync(x => x.SessionId == session.SessionId, session);
                updated = true;
            } 
            catch(Exception e)
            {
                return updated;
            }
            return updated;
        }

        public async Task<bool> ExtendSession(string JWTToken)
        {
            bool extended = false;
            try
            {
                var sessionToExtend = _sessions.Find(x => x.JWTToken == JWTToken).FirstOrDefaultAsync().Result;
                var idFilter = new BsonDocument("SessionId", sessionToExtend.SessionId);
                sessionToExtend.DateExpired = DateTime.UtcNow.AddMinutes(30);
                await _sessions.ReplaceOneAsync(filter, sessionToExtend);
                extended = true;
            } 
            catch
            {
                return extended;
            }
            return extended;
        }

        public void ExpireAllSessions(string jwtToken)
        {
            throw new NotImplementedException();
        }
    
    }
}
