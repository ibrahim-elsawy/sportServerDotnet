using System.ComponentModel.DataAnnotations;

namespace sportServerDotnet.Controllers.Models.DTOs.Requests
{
    public class RangeRequest
    {
        [Required]
        public int skip {get; set;}
        [Required]
        public int take {get; set;}
    }
}