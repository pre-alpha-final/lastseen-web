using LastSeenWeb.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Controllers
{
	[Route("[controller]/[action]")]
	public class AuthController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AuthController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToPage("/Index");
		}
	}
}
