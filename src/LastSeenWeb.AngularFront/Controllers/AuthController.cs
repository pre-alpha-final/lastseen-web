using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LastSeenWeb.AngularFront.Controllers
{
	[Route("api/auth/")]
	public class AuthController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IWebClientService _webClientService;
		private readonly IConfiguration _configuration;

		public AuthController(SignInManager<ApplicationUser> signInManager, IWebClientService webClientService,
			IConfiguration configuration)
		{
			_signInManager = signInManager;
			_webClientService = webClientService;
			_configuration = configuration;
		}

		[HttpPost("login")]
		public async Task<IActionResult> LogIn([FromForm] Credentials credentials)
		{
			var authority = $"{_configuration["Authority"]}/connect/token";
			var clientSecret = _configuration["ClientSecret"];
			var webClientResult = await _webClientService.Post(authority,
				new NameValueCollection
				{
					{ "grant_type", "password" },
					{ "username", credentials.Login },
					{ "password", credentials.Password },
					{ "scope", "lastseenapi" },
					{ "client_id", "lastseen" },
					{ "client_secret", clientSecret },
				});
			if (webClientResult.Success == false)
			{
				throw new Exception(webClientResult.DebugInfo);
			}

			return Ok(webClientResult.Content);
		}

		[HttpGet("logout")]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();

			return NoContent();
		}

		public class Credentials
		{
			public string Login { get; set; }
			public string Password { get; set; }
		}
	}
}
