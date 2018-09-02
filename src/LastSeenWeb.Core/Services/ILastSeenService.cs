using LastSeenWeb.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LastSeenWeb.Core.Services
{
	public interface ILastSeenService
	{
		Task<List<LastSeenItem>> GetAll(string ownerName);
		Task<LastSeenItem> Get(string id, string ownerName);
	}
}
