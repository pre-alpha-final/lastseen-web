using LastSeenWeb.Core.Services;
using LastSeenWeb.Domain.Models;
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
			if (string.IsNullOrWhiteSpace(id) || id == "undefined")
			{
				return View(new LastSeenItem
				{
					MoveToTop = true,
				});
			}

			var domain = await _lastSeenService.Get(id, HttpContext.User.Identity.Name);
			domain.MoveToTop = true;

			return View(domain);
		}
	}
}
