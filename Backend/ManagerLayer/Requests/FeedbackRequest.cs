using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class FeedbackRequest
    {
        [Required]
        public int Category { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Feedback { get; set; }
    }
}
