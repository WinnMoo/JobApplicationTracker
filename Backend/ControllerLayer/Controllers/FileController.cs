﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerLayer.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public ActionResult Upload()
        {
            throw new NotImplementedException();
        }

        public ActionResult Export()
        {
            throw new NotImplementedException();
        }
    }
}