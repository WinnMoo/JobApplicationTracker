﻿using DataAccessLayer.Interfaces;
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
            return _sessionRepo.AddSession(session).Result;
        }
        public bool DeleteSession(Session session)
        {
            return _sessionRepo.DeleteSession(session).Result;
        }
        public bool UpdateSession(Session session)
        {
            return _sessionRepo.UpdateSession(session).Result;
        }
        public Session GetSession(string jwtToken)
        {
            return _sessionRepo.GetSession(jwtToken).Result;
        }

        public bool ExtendSession(string jwtToken)
        {
            return _sessionRepo.ExtendSession(jwtToken).Result;
        }
    }
}
