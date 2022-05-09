using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;




namespace sportServerDotnet.Controllers.Models
{
    public class Support
    {
        public int Id {get; set;}
        public int PostId {get; set;}
        public string UserId { get; set; }


        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

	}
}