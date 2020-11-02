using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerLayer.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly string MONGODB_CONNECTION_STRING = Environment.GetEnvironmentVariable(
            "MongoDB_ConnectionString", EnvironmentVariableTarget.User);

        [HttpPost]
        [Route("api/contact/feedback")]
        public ActionResult Feedback([FromBody] FeedbackRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("api/contact/contact")]
        public ActionResult Contact()
        {
            throw new NotImplementedException();
        }
    }
}
