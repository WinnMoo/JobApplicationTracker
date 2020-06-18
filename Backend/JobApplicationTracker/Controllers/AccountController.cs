using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using ManagerLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer.Requests;

namespace ControllerLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserAccountManager _userAccountManager;
        private MongoClient dbClient;

        public AccountController()
        {
            _userAccountManager = new UserAccountManager(dbClient);
        }

        [HttpPost]
        [Route("api/account/create")]
        public ActionResult CreateAccount([FromBody] AccountRequest request)
        {
            try 
            {
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

    }
}
