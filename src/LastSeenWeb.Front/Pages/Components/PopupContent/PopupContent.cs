using LastSeenWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.PopupContent
{
	public class PopupContent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(string id)
		{
			var domain = new LastSeenItem
			{
				Id = id,
			};

			return Task.FromResult<IViewComponentResult>(View(domain));
		}
	}
}
