using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace LastSeenWeb.Data.Identity
{
	public class Config
	{
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("lastseenapi", "Last Seen API")
			};
		}

		public static IEnumerable<Client> GetClients(string clientSecret)
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "lastseen",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					ClientSecrets =
					{
						new Secret(clientSecret.Sha256())
					},
					AccessTokenLifetime = (int) TimeSpan.FromHours(24).TotalSeconds,
					AllowedScopes = {
						"lastseenapi"
					}
				}
			};
		}
	}
}
