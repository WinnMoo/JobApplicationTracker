using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string PasswordResetToken { get; set; }
        [Required]
        public string EmailAddress { get; set; }
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
