using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.PopupContent
{
	public class PopupContent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(string id)
		{
			var popupContentModel = new PopupContentModel
			{
				Id = id,
			};

			return Task.FromResult<IViewComponentResult>(View(popupContentModel));
		}
	}
}
