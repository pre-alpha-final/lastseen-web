using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.Pagination
{
	public class Pagination : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(PaginationModel model)
		{
			return Task.FromResult<IViewComponentResult>(View(model));
		}
	}
}
