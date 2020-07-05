using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.Requests
{
    public class GetJobApplicationsRequest
    {
        [Required]
        string EmailAddress { get; set; }
        [Required]
        int StartIndex { get; set; }
        [Required]
        int NumOfItemsToGet { get; set; }
    }
}
