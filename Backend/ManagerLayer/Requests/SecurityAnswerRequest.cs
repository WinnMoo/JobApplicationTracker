using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class SecurityAnswerRequest
    {
        [Required]
        public string PasswordResetToken { get; set; }
        [Required]
        public string SecurityAnswer1 { get; set; }
        [Required]
        public string SecurityAnswer2 { get; set; }
        [Required]
        public string SecurityAnswer3 { get; set; }
    }
}
