using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerLayer.Requests
{
    public class GetJobApplicationsRequest
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public int StartIndex { get; set; }
        [Required]
        public int NumOfItemsToGet { get; set; }
    }
}
