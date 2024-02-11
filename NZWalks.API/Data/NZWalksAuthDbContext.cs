using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
	public class NZWalksAuthDbContext : IdentityDbContext
	{
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "a18173a1-464f-4f46-8f97-af989697d8a3";
			var writerRoleId = "c1beb340-47ca-402b-bbf0-2c151b7f1eb5";

			var roles = new List<IdentityRole>
			{
				new IdentityRole()
				{
					Id = readerRoleId,
					ConcurrencyStamp = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper(),
				},
				new IdentityRole()
				{
					Id = writerRoleId,
					ConcurrencyStamp = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper(),
				}
			};
			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
