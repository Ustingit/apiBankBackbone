using ApiBankBackBone.Models.Apis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiBankBackBone.Data
{
	public sealed class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Api> Apis { get; set; }
		public DbSet<Method> Methods { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
