﻿using LastSeenWeb.Domain.Models;
using LastSeenWeb.Core.Services;
using LastSeenWeb.Front.Pages.Components.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LastSeenWeb.Front.Pages
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ILastSeenService _lastSeenService;

		[BindProperty(SupportsGet = true)]
		public int? PageNumber { get; set; }

		public List<LastSeenItem> Items { get; set; }
		public bool HasItems => Items?.Count > 0;
		public PaginationModel PaginationModel { get; set; }

		public IndexModel(ILastSeenService lastSeenService)
		{
			_lastSeenService = lastSeenService;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			PageNumber = PageNumber ?? 1;

			Items = await _lastSeenService.Get();

			PaginationModel = new PaginationModel
			{
				ItemCount = 33,
				PageNumber = (int)PageNumber
			};

			return Page();
		}
	}
}
