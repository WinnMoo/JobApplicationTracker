using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class GetJobPostingsRequest
    {
        [Required]
        string EmailAddress { get; set; }
        [Required]
        int StartIndex { get; set; }
        [Required]
        int NumOfItemsToGet { get; set; }
        string JobTitle { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
    }
}
