using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    public class Log
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int severity { get; set; }
        [Required]
        public string location { get; set; }
    }
}
