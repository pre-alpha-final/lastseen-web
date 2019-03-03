using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LastSeenWeb.AngularFront.Controllers.Models;
using LastSeenWeb.AngularFront.Services;
using LastSeenWeb.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers
{
	[Route("api/auth/")]
	public class AuthController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpClientService _httpClientService;
		private readonly IConfiguration _configuration;

		public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
			IHttpClientService httpClientService, IConfiguration configuration)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_httpClientService = httpClientService;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationCredentials registrationCredentials)
		{
			//Dummy user
			//_userManager.PasswordValidators.Clear();
			//var user = new ApplicationUser { UserName = "username" };
			//var result = await _userManager.CreateAsync(user, "password");
			//await _userManager.AddClaimAsync(user, new Claim("LastSeenApiAccess", "true"));

			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> LogIn([FromBody] LoginCredentials loginCredentials)
		{
			var authority = $"{_configuration["Authority"]}/connect/token";
			var clientSecret = _configuration["ClientSecret"];
			var httpResponseMessage = await _httpClientService.Post(authority,
				new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "password" },
					{ "username", loginCredentials.Login },
					{ "password", loginCredentials.Password },
					{ "scope", "lastseenapi offline_access" },
					{ "client_id", "lastseen" },
					{ "client_secret", clientSecret },
				}));

			return StatusCode((int)httpResponseMessage.StatusCode,
				await httpResponseMessage.Content.ReadAsStringAsync());
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh([FromBody] RefreshTokenResponse refreshToken)
		{
			var authority = $"{_configuration["Authority"]}/connect/token";
			var clientSecret = _configuration["ClientSecret"];
			var httpResponseMessage = await _httpClientService.Post(authority,
				new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "refresh_token" },
					{ "refresh_token", refreshToken.RefreshToken },
					{ "scope", "lastseenapi" },
					{ "client_id", "lastseen" },
					{ "client_secret", clientSecret },
				}));

			return StatusCode((int)httpResponseMessage.StatusCode,
				await httpResponseMessage.Content.ReadAsStringAsync());
		}

		[HttpGet("logout")]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();

			return NoContent();
		}

		[HttpGet("checkemail")]
		public async Task<IActionResult> CheckEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return Ok(JsonConvert.SerializeObject(new CheckEmailResponse
				{
					ErrorMessage = "Invalid arguments",
				}));
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return Ok(JsonConvert.SerializeObject(new CheckEmailResponse
				{
					ErrorMessage = $"Unable to load user with ID '{userId}'",
				}));
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);
			if (result.Succeeded == false)
			{
				return Ok(JsonConvert.SerializeObject(new CheckEmailResponse
				{
					ErrorMessage = $"Error confirming email for user with ID '{userId}'",
				}));
			}

			return Ok(JsonConvert.SerializeObject(new CheckEmailResponse
			{
				SuccessMessage = "Thank You for confirming Your email",
			}));
		}
	}
}
