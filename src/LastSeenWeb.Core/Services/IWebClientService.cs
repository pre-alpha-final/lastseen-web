using LastSeenWeb.Core.Infrastructure;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace LastSeenWeb.Core.Services
{
	public interface IWebClientService
	{
		Task<WebClientResult> Get(string url, int retryCount = 5);
		Task<WebClientResult> Post(string url, NameValueCollection values, int retryCount = 5);
	}
}
