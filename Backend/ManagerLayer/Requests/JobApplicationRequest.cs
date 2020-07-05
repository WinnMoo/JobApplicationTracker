using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class JobApplicationRequest
    {
        [Required]
        string CompanyName { get; set; }
        [Required]
        string JobTitle { get; set; }
        [Required]
        string Description { get; set; }

        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
    }
}
