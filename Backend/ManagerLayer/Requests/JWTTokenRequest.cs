using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class JWTTokenRequest
    {
        [Required]
        public string JWTToken { get; set; }
    }
}
