using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerLayer.Controllers
{
    public class JobAppController : Controller
    {
        [HttpPost]
        [Route("api/jobapp/update")]
        public IHttpActionResult UpdateJobApplications([FromBody] JobAppRequest request)
        {

        }
    }
}
