using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerLayer.Managers;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        public ActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                SessionManager _sessionManager = new SessionManager(new MongoClient(Environment.GetEnvironmentVariable(MONGODB_CONNECTION_STRING)));
                return _sessionManager.Login(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public ActionResult LogOut(JWTTokenRequest request)
        {
            try
            {
                SessionManager _sessionManager = new SessionManager(new MongoClient(Environment.GetEnvironmentVariable(MONGODB_CONNECTION_STRING)));
                return _sessionManager.LogOut(request.JWTToken);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
