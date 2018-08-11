using LastSeenWeb.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LastSeenWeb.Data.Services
{
	public interface ILastSeenRepository
	{
		Task<List<LastSeenItem>> Get();
	}
}
