using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LastSeenWeb.Front.Pages.Auth
{
	public class ForgotPasswordConfirmationModel : PageModel
	{
		public Task OnGetAsync()
		{
			return Task.FromResult(Page());
		}
	}
}