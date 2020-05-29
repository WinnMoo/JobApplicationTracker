using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace ServiceLayer.Requests
{
    public class AccountRequest
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string SecurityQuestion1 { get; set; }
        [Required]
        public string SecurityQuestion2 { get; set; }
        [Required]
        public string SecurityQuestion3 { get; set; }
        [Required]
        public string SecurityAnswer1 { get; set; }
        [Required]
        public string SecurityAnswer2 { get; set; }
        [Required]
        public string SecurityAnswer3 { get; set; }

    }
}
