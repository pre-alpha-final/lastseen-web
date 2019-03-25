using System.Linq;
using System.Threading.Tasks;
using LastSeenWeb.AngularFront.Controllers.Models;
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

		[HttpPut]
		public async Task<IActionResult> Upsert([FromBody] LastSeenItemModel model)
		{
			var domain = new LastSeenItem
			{
				Id = model.Id,
				Season = model.Season,
				Episode = model.Episode,
				VisitUrl = model.VisitUrl,
				Unfinished = model.Unfinished,
				Hours = model.Hours,
				Minutes = model.Minutes,
				Seconds = model.Seconds,
				Notes = model.Notes,
				MoveToTop = model.MoveToTop,
				Name = model.Name,
				ImageUrl = model.ImageUrl,
				TrackingUrl = model.TrackingUrl,
				EpisodesBehind = model.EpisodesBehind,
			};
			await _lastSeenService.Upsert(domain, GetUsername());

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var items = await _lastSeenService.GetAll(GetUsername());

			return Ok(items);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var item = await _lastSeenService.Get(id, GetUsername());

			return Ok(item);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _lastSeenService.Delete(id, GetUsername());

			return NoContent();
		}

		private string GetUsername()
		{
			return HttpContext.User.Claims.FirstOrDefault(e => e.Type == "username")?.Value ?? "";
		}
	}
}
