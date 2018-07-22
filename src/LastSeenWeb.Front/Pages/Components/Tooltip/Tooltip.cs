using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.Tooltip
{
	public class Tooltip : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(TooltipModel model)
		{
			return Task.FromResult<IViewComponentResult>(View(model));
		}
	}
}
