using LastSeenWeb.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LastSeenWeb.Core.Services
{
	public interface ILastSeenService
	{
		Task<List<LastSeenItem>> Get();
	}
}
