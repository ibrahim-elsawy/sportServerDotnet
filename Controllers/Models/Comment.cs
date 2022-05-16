using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;




namespace sportServerDotnet.Controllers.Models
{
    public class Comment
    {
        public int Id {get; set;}
        public string Text { get; set; }
		public int PostId {get; set;}
        public string UserId { get; set; }
        public ICollection<Replies> Replies{ get; set; }
		public ICollection<Support> Supports { get; set; }


		[ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        

    }
}