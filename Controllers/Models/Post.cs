using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;



namespace sportServerDotnet.Controllers.Models
{
    public class Post
    {
        public int Id { get; set; }
		public string UserId { get; set; }
        public string Text { get; set; }
        public string Image {get; set;}
        public ICollection<Dashboard> Dashboards { get; set; }
		public ICollection<Support> Supports { get; set; }
        public ICollection<Comment> Comments { get; set; }

		public DateTime AddedDate { get; set; }

		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; }
    }
}