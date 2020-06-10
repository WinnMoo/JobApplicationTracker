using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Requests;

namespace ControllerLayer.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [Route("api/account/create")]
        public ActionResult CreateAccount([FromBody] AccountRequest request)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("api/account/deletea")]
        public ActionResult DeleteAccount([FromBody] AccountRequest request)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("api/account/update")]
        public ActionResult UpdateAccount([FromBody] AccountRequest request)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("api/account/get")]
        public ActionResult UpdateAccount([FromBody] string Username)
        {
            throw new NotImplementedException();
        }

    }
}
