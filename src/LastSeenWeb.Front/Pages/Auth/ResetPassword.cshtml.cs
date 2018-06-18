using LastSeenWeb.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Auth
{
	public class ResetPasswordModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		[Required]
		[EmailAddress]
		[BindProperty]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[BindProperty]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		[BindProperty]
		public string ConfirmPassword { get; set; }

		[BindProperty]
		public string Code { get; set; }

		public ResetPasswordModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public Task<IActionResult> OnGetAsync(string code = null)
		{
			if (code == null)
			{
				throw new ApplicationException("A code must be supplied for password reset.");
			}
			else
			{
				Code = code;
				return Task.FromResult((IActionResult)Page());
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _userManager.FindByEmailAsync(Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return RedirectToPage("./ResetPasswordConfirmation");
			}

			var result = await _userManager.ResetPasswordAsync(user, Code, Password);
			if (result.Succeeded)
			{
				return RedirectToPage("./ResetPasswordConfirmation");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return Page();
		}
	}
}
