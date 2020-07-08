using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface ISessionRepository
    {
        public bool AddSession(Session session);
        public bool DeleteSession(Session session);
        public bool UpdateSession(Session session);
        public Session GetSession(string jwtToken);

        public void ExpireAllSessions(string emailAddress);
    }
}
