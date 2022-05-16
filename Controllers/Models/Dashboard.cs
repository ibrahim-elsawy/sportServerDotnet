using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;




namespace sportServerDotnet.Controllers.Models
{
    public class Dashboard
    {
        public int Id { get; set; }
		public string UserId { get; set; }
        public ICollection<Post> Posts { get; set; }

		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; }
    }
}