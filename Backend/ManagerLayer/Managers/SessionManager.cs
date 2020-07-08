using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Managers
{
    public class SessionManager
    {
        private SessionService _sessionService;

        public SessionManager(MongoClient _db)
        {
            _sessionService = new SessionService(_db);
        }

        public ActionResult Login()
        {
            throw new NotImplementedException();
        }

        public ActionResult LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
