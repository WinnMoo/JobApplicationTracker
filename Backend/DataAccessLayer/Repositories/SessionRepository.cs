using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

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
        public bool AddSession(Session session)
        {
            bool Added = false;
            try
            {
                _sessions.InsertOne(session);
                Added = true;
            } catch (Exception e)
            {
                return Added;
            }
            return Added;
        }

        public bool DeleteSession(Session session)
        {
            bool Deleted = false;
            var filter = new BsonDocument("SessionId", session.SessionId);
            try
            {
                _sessions.DeleteOne(filter);
                Deleted = true;
            }
            catch
            {
                return Deleted;
            }
            return Deleted;
        }

        public Session GetSession(string emailAddress)
        {
            var filter = new BsonDocument("EmailAddress", emailAddress);
            try
            {
                return _sessions.Find(filter).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateSession(Session session)
        {
            bool updated = false;
            var filter = new BsonDocument("SessionId", session.SessionId);
            try
            {
                _sessions.ReplaceOne(filter, session);
                updated = true;
            } catch(Exception e)
            {
                return updated;
            }
            return updated;
        }

        public void ExpireAllSessions(string emailAddress)
        {
            throw new NotImplementedException();
        }
    
    }
}
