using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class JobPostingRequest
    {
        [Required]
        public string URL { get; set; }
    }
}
