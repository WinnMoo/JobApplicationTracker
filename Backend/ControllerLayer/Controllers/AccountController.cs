using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using ManagerLayer.Managers;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ControllerLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        [Route("api/[controller]")]

        [HttpPost]
        [Route("api/account/create")]
        public ActionResult CreateAccount([FromBody] AccountRequest request)
        {
            try 
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.CreateUserAccount(request);
            } catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("api/account/delete")]
        public ActionResult DeleteAccount([FromBody] LoginRequest request)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.DeleteUserAccount(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("api/account/update")]
        public ActionResult UpdateAccount([FromBody] AccountRequest request)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.UpdateUserAccount(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/account/changepassword")]
        public ActionResult ChangePassword([FromBody] UpdatePasswordRequest request)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.UpdateUserPassword(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/account/get")]
        public ActionResult GetAccount([FromBody] string Username)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("api/account/generateresetlink")]
        public ActionResult GenerateResetLink(string emailAddress)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.GenerateResetPassword(emailAddress);
            } 
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/account/fetchsecurityquestions")]
        public ActionResult FetchSecurityQuestions(string PasswordResetToken)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.FetchSecurityQuestions(PasswordResetToken);
            } 
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/account/checksecurityanswers")]
        public ActionResult CheckSecurityAnswers(SecurityAnswerRequest request)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.CheckSecurityAnswers(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/account/resetpassword")]
        public ActionResult ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                UserAccountManager _userAccountManager = new UserAccountManager(new MongoClient(MONGODB_CONNECTION_STRING));
                return _userAccountManager.ResetPassword(request);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
