using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LastSeenWeb.AngularFront.Controllers
{
	[Authorize(Policy = "LastSeenApiAccess")]
	[Route("api/lastseenitems")]
	public class LastSeenItemsController : Controller
	{
		private readonly ILastSeenService _lastSeenService;

		public LastSeenItemsController(ILastSeenService lastSeenService)
		{
			_lastSeenService = lastSeenService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			//var items = await _lastSeenService.GetAll(HttpContext.User.Claims
			//	.FirstOrDefault(e => e.Type == "username")?.Value ?? "");

			//return Ok(items);

			return Ok(_stub);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			return Ok(_stub.FirstOrDefault(e => e.Id == id));
		}

		private List<LastSeenItem> _stub = new List<LastSeenItem>
		{
			new LastSeenItem { Id = "1", Name = "item 1", Season = 1, Unfinished = true, Hours = 2, Minutes = 3, Notes = "some notes", VisitUrl = "http://google.com", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png" },
			new LastSeenItem { Id = "2", Name = "item 2", Season  = 1, Episode = 1, ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51zLZbEVSTL._SX425_.jpg" },
			new LastSeenItem { Id = "3", Name = "item", Season  = 1, Episode = 1, ImageUrl = "https://i.guim.co.uk/img/static/sys-images/Arts/Arts_/Pictures/2009/2/20/1235150177056/Smiley-001.jpg?width=300&quality=85&auto=format&fit=max&s=5cf88208cce59af2cf03b827227efdff" },
			new LastSeenItem { Id = "4", Name = "item 3", Episode = 1, Notes = "some notes", VisitUrl = "http://google.com", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/220px-SNice.svg.png" },
			new LastSeenItem { Id = "5", Name = "item 4", Unfinished = true, Hours = 1, ImageUrl = "https://spectator.imgix.net/content/uploads/2015/06/Emoji.jpg?auto=compress,enhance,format&crop=faces,entropy,edges&fit=crop&w=620&h=413" },
			new LastSeenItem { Id = "6", Name = "item 5", Unfinished = true, Minutes = 1, VisitUrl = "", ImageUrl = "https://cdn.shopify.com/s/files/1/1061/1924/products/Smiling_Face_Emoji_with_Blushed_Cheeks_large.png?v=1480481056" },
			new LastSeenItem { Id = "7", Name = "item 6", Unfinished = true, Hours = 1, Minutes = 2, ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41q0g8iq5iL.jpg" }
		};
	}
}
