using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages
{
	public class IndexModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		public async Task OnGetAsync()
		{
			PageNumber = PageNumber ?? 1;
		}
	}
}
