using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sportServerDotnet.Controllers.Models;

namespace sportServerDotnet.Data
{
	public class ApiDbContext : IdentityDbContext
	{
		// public virtual DbSet<Testjs> Items { get; set; }
		public virtual DbSet<RefreshToken> RefreshToken { get; set; }
		public virtual DbSet<Dashboard> Dashboards{ get; set; }
		public virtual DbSet<Post> Posts{ get; set; }
		public virtual DbSet<Comment> Comments{ get; set; }
		public virtual DbSet<Replies> Replies{ get; set; }
		public virtual DbSet<Support> Supports{ get; set; }
		public virtual DbSet<UserProfile> Profile{ get; set; }
		public virtual DbSet<Challenge> Challenge{ get; set; }

		public ApiDbContext(DbContextOptions<ApiDbContext> options)
			: base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder) 
		{
			base.OnModelCreating(builder);
			
			builder.Entity<Dashboard>()
				.HasMany<Post>(d => d.Posts);

			builder.Entity<Post>()
				.HasMany<Support>(p => p.Supports)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);


			builder.Entity<Post>()
				.HasMany<Comment>(p => p.Comments)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Comment>()
				.HasMany<Support>(c => c.Supports)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Comment>()
				.HasMany<Replies>(c => c.Replies)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Replies>()
				.HasMany<Support>(r => r.Supports)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}