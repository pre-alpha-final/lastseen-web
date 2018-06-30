using LastSeenWeb.Front.Pages.Components.PopupContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Controllers
{
	[Route("[controller]/[action]")]
	[Authorize]
	public class PopupController : Controller
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> Content(int id)
		{
			return ViewComponent(nameof(PopupContent), id);
		}
	}
}
