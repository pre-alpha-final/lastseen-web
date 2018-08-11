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

		public Task<List<LastSeenItem>> Get()
		{
			return _lastSeenRepository.Get();
		}
	}
}
