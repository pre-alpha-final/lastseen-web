using Microsoft.AspNetCore.Identity;
using System;

namespace LastSeenWeb.Data.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public Guid OriginProductId { get; set; } = new Guid("08539f34-606e-4ec2-9932-eab320c6cd51"); // LastSeenWeb
	}
}
