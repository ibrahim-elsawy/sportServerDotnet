using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace sportServerDotnet.Controllers.Models.DTOs.Requests
{
    public class CreateChallengeRequest
    {
        [Required]
        public string description {get; set;}
        [Required]
        public IFormFile gif { get; set; }

	}
}