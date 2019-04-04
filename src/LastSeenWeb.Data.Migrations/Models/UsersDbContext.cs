using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LastSeenWeb.Data.Migrations.Models
{
	public class UsersDbContext : IdentityDbContext<ApplicationUser>
	{
		private const string ConnectionString = "";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseMySql(ConnectionString);
		}
	}
}
