using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sportServerDotnet.Controllers.Models;

namespace sportServerDotnet.Data
{
	public class ApiDbContext : IdentityDbContext
	{
		// public virtual DbSet<Testjs> Items { get; set; }
		public virtual DbSet<RefreshToken> RefreshToken { get; set; }
		public virtual DbSet<Person> Person { get; set; }

		public ApiDbContext(DbContextOptions<ApiDbContext> options)
			: base(options)
		{

		}
	}
}