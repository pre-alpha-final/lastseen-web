using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.Pagination
{
	public class Pagination : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = new Model();
			model.PageNumber = 2;
			model.ItemCount = 30;

			return View(model);
		}
	}
}
