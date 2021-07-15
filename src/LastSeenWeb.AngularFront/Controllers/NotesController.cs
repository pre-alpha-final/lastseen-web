using System.Linq;
using System.Threading.Tasks;
using LastSeenWeb.AngularFront.Controllers.Models;
using LastSeenWeb.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LastSeenWeb.AngularFront.Controllers
{
	[Authorize(Policy = "LastSeenApiAccess")]
	[Route("api/notes")]
	public class NotesController : Controller
	{
		private readonly ILastSeenService _lastSeenService;

		public NotesController(ILastSeenService lastSeenService)
		{
			_lastSeenService = lastSeenService;
		}

		[HttpPut]
		public async Task<IActionResult> Upsert([FromBody] NotesModel model)
		{
			await _lastSeenService.UpsertNotes(model.Notes, GetUsername());

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetNotes()
		{
			var notes = await _lastSeenService.GetNotes(GetUsername());

			return Ok(notes);
		}

		private string GetUsername()
		{
			return HttpContext.User.Claims.FirstOrDefault(e => e.Type == "username")?.Value ?? "";
		}
	}
}
