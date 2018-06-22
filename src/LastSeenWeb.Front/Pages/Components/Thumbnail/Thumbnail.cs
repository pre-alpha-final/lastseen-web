using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.Thumbnail
{
	public class Thumbnail : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
