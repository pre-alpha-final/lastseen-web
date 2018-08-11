using LastSeenWeb.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.MasonryList
{
	public class MasonryList : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(List<LastSeenItem> lastSeenItems)
		{
			return Task.FromResult<IViewComponentResult>(View(lastSeenItems));
		}
	}
}
