using LastSeenWeb.Core.Services;
using LastSeenWeb.Data.Identity.Models;
using LastSeenWeb.Front.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Auth
{
	public class RegisterModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;

		[Required]
		[EmailAddress]
		[BindProperty]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[BindProperty]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
		[BindProperty]
		public string ConfirmPassword { get; set; }

		public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
		}

		public Task OnGetAsync()
		{
			// Dummy user
			//_userManager.PasswordValidators.Clear();
			//var result = await _userManager.CreateAsync(new ApplicationUser { UserName = "username" }, "password");

			return Task.FromResult(Page());
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = Email, Email = Email };
				var result = await _userManager.CreateAsync(user, Password);
				if (result.Succeeded)
				{
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
					await _emailSender.SendEmailConfirmationAsync(Email, callbackUrl);

					await _signInManager.SignInAsync(user, false);
					return LocalRedirect("/");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			ModelState.AddModelError(string.Empty, "Registration failed");
			return Page();
		}
	}
}
