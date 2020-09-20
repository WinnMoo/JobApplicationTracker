using DataAccessLayer.Models;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Managers
{
    public class SessionManager
    {
        private readonly SessionService _sessionService;
        private readonly UserAccountService _userAccountService;
        private readonly PasswordService _passwordService;

        public SessionManager(MongoClient _db)
        {
            _sessionService = new SessionService(_db);
            _userAccountService = new UserAccountService(_db);
            _passwordService = new PasswordService();
        }

        public ActionResult Login(LoginRequest request)
        {
            // Steps for Implementation
            // 1. Check if user exists
            // 1. Create JWT Token
            // 2. Create Session
            // 3. Add Session to DB
            // 4. Return JWT Token on success
            var user = _userAccountService.ReadUserFromDBUsingEmail(request.EmailAddress.ToLower());
            if(user == null)
            {
                return new BadRequestObjectResult("User not found.");
            }
            if (!_passwordService.ValidatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                // Limit attempts
                // Invalidate all sessions
                return new BadRequestObjectResult("Incorrect password.");
            }
            string jwtToken = JWTService.CreateToken();
            Session session = new Session(user.Email, jwtToken);

            if (!_sessionService.AddSession(session))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new OkObjectResult(jwtToken);
        }

        public ActionResult LogOut(string jwtToken)
        {
            // Expire the session in the DB that is active and has the given session token
            var session = InvalidateSession(jwtToken);
            if(session == null)
            {
                return new BadRequestObjectResult("Invalid session token");
            }

            if (!_sessionService.UpdateSession(session))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new OkObjectResult("Successfully Logged Out");
        }

        public Session InvalidateSession(string jwtToken)
        {
            var session = _sessionService.GetSession(jwtToken);
            if(session != null)
            {
                session.DateExpired = DateTime.Now;
                return session;
            }
            return null;
        }
    }
}
