using LastSeenWeb.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.PopupContent
{
	public class PopupContent : ViewComponent
	{
		private readonly ILastSeenService _lastSeenService;

		public PopupContent(ILastSeenService lastSeenService)
		{
			_lastSeenService = lastSeenService;
		}

		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			var domain = await _lastSeenService.Get(id, null);

			return View(domain);
		}
	}
}
