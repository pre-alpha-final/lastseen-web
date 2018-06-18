using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LastSeenWeb.Data.Identity.Models
{
	public class UsersDbContext : IdentityDbContext<ApplicationUser>
	{
		private readonly IConfiguration _configuration;

		public UsersDbContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseMySql(_configuration.GetConnectionString("UsersDbContext"));
		}
	}
}
