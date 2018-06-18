using LastSeenWeb.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Auth
{
	public class LoginModel : PageModel
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		[Required]
		[EmailAddress]
		[BindProperty]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[BindProperty]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }

		public LoginModel(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public Task OnGetAsync()
		{
			return Task.CompletedTask;
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
		{
			if (!ModelState.IsValid)
				return Page();

			var result = await _signInManager.PasswordSignInAsync(Email, Password, false, false);
			if (result.Succeeded)
			{
				return LocalRedirect(returnUrl);
			}

			ModelState.AddModelError(string.Empty, "Invalid login attempt");
			return Page();
		}
	}
}
