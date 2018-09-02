using LastSeenWeb.Core.Services;
using LastSeenWeb.Domain.Models;
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
		private readonly ILastSeenService _lastSeenService;

		public PopupContentController(ILastSeenService lastSeenService)
		{
			_lastSeenService = lastSeenService;
		}

		[HttpGet("{id}")]
		public Task<IActionResult> OnGetAsync(string id)
		{
			return Task.FromResult<IActionResult>(ViewComponent(nameof(PopupContent), id));
		}

		public async Task<IActionResult> OnPostAsync(LastSeenItem model)
		{
			await _lastSeenService.Upsert(model, HttpContext.User.Identity.Name);

			return Ok();
		}
	}
}
