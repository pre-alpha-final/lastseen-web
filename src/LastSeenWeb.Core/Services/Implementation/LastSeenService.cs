using LastSeenWeb.Data.Services;
using LastSeenWeb.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LastSeenWeb.Core.Services.Implementation
{
	public class LastSeenService : ILastSeenService
	{
		private readonly ILastSeenRepository _lastSeenRepository;

		public LastSeenService(ILastSeenRepository lastSeenRepository)
		{
			_lastSeenRepository = lastSeenRepository;
		}

		public Task<LastSeenItem> Get(string id, string ownerName)
		{
			return _lastSeenRepository.Get(id, ownerName);
		}

		public Task<List<LastSeenItem>> GetAll(string ownerName)
		{
			return _lastSeenRepository.GetAll(ownerName);
		}

		public async Task Upsert(LastSeenItem lastSeenItem, string ownerName)
		{
			if (new ApplicationSettings().DemoUsername == ownerName)
			{
				return;
			}

			if (lastSeenItem.Unfinished == false)
			{
				lastSeenItem.Hours = 0;
				lastSeenItem.Minutes = 0;
				lastSeenItem.Seconds = 0;
			}

			await _lastSeenRepository.Upsert(lastSeenItem, ownerName);
		}

		public Task Delete(string id, string ownerName)
		{
			return new ApplicationSettings().DemoUsername == ownerName
				? Task.CompletedTask
				: _lastSeenRepository.Delete(id, ownerName);
		}
	}
}
