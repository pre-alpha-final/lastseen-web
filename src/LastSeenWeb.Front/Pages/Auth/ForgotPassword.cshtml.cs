using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Data.Identity.Models;
using LastSeenWeb.Front.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LastSeenWeb.Front.Pages.Auth
{
	public class ForgotPasswordModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;

		[Required]
		[EmailAddress]
		[BindProperty]
		public string Email { get; set; }

		public ForgotPasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(Email);
				if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
				{
					// Don't reveal that the user does not exist or is not confirmed
					return RedirectToPage("./ForgotPasswordConfirmation");
				}

				var code = await _userManager.GeneratePasswordResetTokenAsync(user);
				var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
				await _emailSender.SendResetPasswordAsync(Email, callbackUrl);
				return RedirectToPage("./ForgotPasswordConfirmation");
			}

			return Page();
		}
	}
}
