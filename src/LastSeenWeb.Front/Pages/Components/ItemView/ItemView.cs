using LastSeenWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.ItemView
{
	public class ItemView : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(LastSeenItem lastSeenItem)
		{
			return Task.FromResult<IViewComponentResult>(View(lastSeenItem));
		}
	}
}
