using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ISessionRepository
    {
        public Task<bool> AddSession(Session session);
        public Task<bool> DeleteSession(Session session);
        public Task<bool> UpdateSession(Session session);
        public Task<Session> GetSession(string jwtToken);
        public Task<bool> ExtendSession(string jwtToken);
        public void ExpireAllSessions(string emailAddress);
    }
}
