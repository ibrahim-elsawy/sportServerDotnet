using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace sportServerDotnet.Controllers.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Admin_Id { get; set; }
		public List<Post> Posts {get; set;}
        public List<string> Users_Id{ get; set; }

    }
}