using LastSeenWeb.Front.Pages.Components.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		public PaginationModel PaginationModel { get; set; }

		public async Task OnGetAsync()
		{
			PageNumber = PageNumber ?? 1;

			PaginationModel = new PaginationModel
			{
				ItemCount = 33,
				PageNumber = (int)PageNumber
			};
		}
	}
}
