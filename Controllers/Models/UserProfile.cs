using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;



namespace sportServerDotnet.Controllers.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
		public string Image { get; set; }
        public string Bio { get; set; }
        public List<string> Followers{ get; set; }
        public List<string> Following{ get; set; }

		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; }

	}
}