using System;
using ManagerLayer.Managers;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ControllerLayer.Controllers
{
    public class SessionController : ControllerBase
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        [HttpPost]
        [Route("api/session/login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                SessionManager _sessionManager = new SessionManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _sessionManager.Login(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/session/logout")]
        public ActionResult LogOut([FromBody] JWTTokenRequest request)
        {
            try
            {
                SessionManager _sessionManager = new SessionManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _sessionManager.LogOut(request.JWTToken);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
