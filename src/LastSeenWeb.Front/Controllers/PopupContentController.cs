using LastSeenWeb.Front.Pages.Components.PopupContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class PopupContentController : Controller
	{
		[HttpGet("{id}")]
		public Task<IActionResult> OnGetAsync(string id)
		{
			return Task.FromResult<IActionResult>(ViewComponent(nameof(PopupContent), id));
		}
	}
}
