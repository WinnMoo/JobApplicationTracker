using System.ComponentModel.DataAnnotations;

namespace ManagerLayer.Requests
{
    public class DeleteJobApplicationRequest
    {
        [Required]
        public string JobApplicationId {get; set;}
    }
}
