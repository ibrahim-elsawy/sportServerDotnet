using System.ComponentModel.DataAnnotations;

namespace sportServerDotnet.Controllers.Models.DTOs.Requests
{
    public class CommentRequest
    {
        [Required]
        public string Text {get; set;}
        [Required]
        public int Post_Id { get; set; }
	}
}