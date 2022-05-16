using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;




namespace sportServerDotnet.Controllers.Models
{
    public class Support
    {
        public int Id {get; set;}
        // public int PostId {get; set;}
        // public int CommentId {get; set;}
        // public int ReplyId {get; set;}
        public string UserId { get; set; }
    

        // [ForeignKey(nameof(PostId))]
        // public Post Post { get; set; }
        // [ForeignKey(nameof(CommentId))]
        // public Comment Comment{ get; set; }
        // [ForeignKey(nameof(ReplyId))]
        // public Replies Reply { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

	}
}