using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LastSeenWeb.AngularFront.Controllers.Models;
using LastSeenWeb.AngularFront.Services;
using LastSeenWeb.Core.Services;
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
		private readonly IEmailSender _emailSender;

		public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
			IHttpClientService httpClientService, IConfiguration configuration, IEmailSender _emailSender)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_httpClientService = httpClientService;
			_configuration = configuration;
			this._emailSender = _emailSender;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			//Dummy user
			//_userManager.PasswordValidators.Clear();
			//var user = new ApplicationUser { UserName = "username" };
			//var result = await _userManager.CreateAsync(user, "password");
			//await _userManager.AddClaimAsync(user, new Claim("LastSeenApiAccess", "true"));

			if (model.Password != model.Password2)
			{
				return Ok(new ErrorResponse { Error = "Passwords don't match" });
			}

			var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var callbackUrl = Url.Page("/auth/emailconfirmation", null,
					new { user.Id, code }, Request.Scheme);
				await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

				await _signInManager.SignInAsync(user, false);

				return Ok();
			}

			return BadRequest(new ErrorResponse
			{
				Error = string.Join(", ", result.Errors.Select(e => e.Description))
			});
		}

		[HttpPost("login")]
		public async Task<IActionResult> LogIn([FromBody] LoginModel model)
		{
			var authority = $"{_configuration["Authority"]}/connect/token";
			var clientSecret = _configuration["ClientSecret"];
			var httpResponseMessage = await _httpClientService.Post(authority,
				new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "password" },
					{ "username", model.Login },
					{ "password", model.Password },
					{ "scope", "lastseenapi offline_access" },
					{ "client_id", "lastseen" },
					{ "client_secret", clientSecret },
				}));

			return StatusCode((int)httpResponseMessage.StatusCode,
				await httpResponseMessage.Content.ReadAsStringAsync());
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh([FromBody] RefreshTokenResponse model)
		{
			var authority = $"{_configuration["Authority"]}/connect/token";
			var clientSecret = _configuration["ClientSecret"];
			var httpResponseMessage = await _httpClientService.Post(authority,
				new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{ "grant_type", "refresh_token" },
					{ "refresh_token", model.RefreshToken },
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
				return Ok(JsonConvert.SerializeObject(new ErrorResponse
				{
					Error = "Invalid arguments",
				}));
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return Ok(JsonConvert.SerializeObject(new ErrorResponse
				{
					Error = $"Unable to load user with ID '{userId}'",
				}));
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);
			if (result.Succeeded == false)
			{
				return Ok(JsonConvert.SerializeObject(new ErrorResponse
				{
					Error = $"Error confirming email for user with ID '{userId}'",
				}));
			}

			return Ok();
		}

		[HttpPost("forgotpassword")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
			{
				return Ok(JsonConvert.SerializeObject(new ErrorResponse
				{
					Error = "User does not exist or email unconfirmed"
				}));
			}

			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			var callbackUrl = Url.Page("/auth/resetpassword", null,
				new { user.Id, code }, Request.Scheme);
			await _emailSender.SendResetPasswordAsync(model.Email, callbackUrl);

			return Ok();
		}
	}
}
