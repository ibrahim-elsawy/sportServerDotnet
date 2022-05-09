using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;



namespace sportServerDotnet.Controllers.Models
{
    public class Replies
    {
        public int Id {get; set;}
        public string Text { get; set; }
		public int PostId {get; set;}
        public string UserId { get; set; }
		public List<Support> Supports { get; set; }
        public int CommentId { get; set; }

		[ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment comment{ get; set; }
    }
}