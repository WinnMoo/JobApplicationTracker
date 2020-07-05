using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
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
