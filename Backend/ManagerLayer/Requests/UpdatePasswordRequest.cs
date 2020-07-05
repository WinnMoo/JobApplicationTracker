using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace ManagerLayer.Requests
{
    public class UpdatePasswordRequest
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string SecurityAnswer1 { get; set; }
        [Required]
        public string SecurityAnswer2 { get; set; }
        [Required]
        public string SecurityAnswer3 { get; set; }
    }
}
