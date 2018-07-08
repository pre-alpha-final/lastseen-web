using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages.Components.PopupContent
{
	public class PopupContent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(int id)
		{
			var popupContentModel = new PopupContentModel
			{
				Name = "foo",
			};

			return Task.FromResult<IViewComponentResult>(View(popupContentModel));
		}
	}
}
