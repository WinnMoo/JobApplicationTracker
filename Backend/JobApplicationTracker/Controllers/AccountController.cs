using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerLayer.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [Route("api/account/create")]
        public IHttpActionResult CreateAccount([FromBody] AccountRequest)
        {

        }
        [HttpPost]
        [Route("api/account/deletea")]
        public IHttpActionResult DeleteAccount([FromBody] AccountRequest)
        {

        }
        [HttpPost]
        [Route("api/account/update")]
        public IHttpActionResult UpdateAccount([FromBody] AccountRequest)
        {

        }
        [HttpGet]
        [Route("api/account/get")]
        public IHttpActionResult UpdateAccount([FromBody] string Username)
        {

        }

    }
}
