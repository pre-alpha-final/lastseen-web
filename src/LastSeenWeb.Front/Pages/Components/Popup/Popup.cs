using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.Popup
{
	public class Popup : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult<IViewComponentResult>(View());
		}
	}
}
