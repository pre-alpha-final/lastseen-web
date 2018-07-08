using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.ItemView
{
	public class ItemView : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult<IViewComponentResult>(View());
		}
	}
}
