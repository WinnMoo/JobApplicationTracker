using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using ManagerLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Requests;

namespace ControllerLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserAccountManager _userAccountManager;

        public AccountController(UserAccountManager userAccountManager)
        {
            _userAccountManager = userAccountManager;
        }

        [HttpPost]
        [Route("api/account/create")]
        public ActionResult CreateAccount([FromBody] AccountRequest request)
        {
            try {
                if (_userAccountManager.CreateUserAccount(request))
                {
                    return CreatedAtRoute("CreateAccount", true);
                }
                else
                {
                    return BadRequest("Unable to create account");
                }
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
                if (_userAccountManager.DeleteUserAccount(request))
                {
                    return CreatedAtRoute("CreateAccount", true);
                }
                else
                {
                    return BadRequest("Unable to delete account");
                }
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
                if (_userAccountManager.UpdateUserAccount(request))
                {
                    return Ok("Updated account information successfully");
                }
                else
                {
                    return BadRequest();
                }
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
                if (_userAccountManager.UpdateUserPassword(request))
                {
                    return Ok("Updated password successfully");
                }
                else
                {
                    return BadRequest();
                }
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
