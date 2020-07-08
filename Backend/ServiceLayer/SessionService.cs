using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class SessionService
    {
        private ISessionRepository _sessionRepo;

        public SessionService(MongoClient _db)
        {
            _sessionRepo = new SessionRepository(_db);
        }
        public bool AddSession(Session session)
        {
            return _sessionRepo.AddSession(session);
        }
        public bool DeleteSession(Session session)
        {
            return _sessionRepo.DeleteSession(session);
        }
        public bool UpdateSession(Session session)
        {
            return _sessionRepo.UpdateSession(session);
        }
        public Session GetSession(string emailAddress)
        {
            return _sessionRepo.GetSession(emailAddress);
        }
    }
}
