﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string PasswordResetToken { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
