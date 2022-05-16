using System.ComponentModel.DataAnnotations;

namespace sportServerDotnet.Controllers.Models.DTOs.Requests
{
    public class ReplyRequest
    {
        [Required]
        public string Text {get; set;}
        [Required]
        public int Comment_Id{ get; set; }
    }
}