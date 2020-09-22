using System.ComponentModel.DataAnnotations;

namespace ManagerLayer.Requests
{
    public class ParseJobPostingRequest
    {
        [Required]
        public string UrlToParse { get; set; }
    }
}
