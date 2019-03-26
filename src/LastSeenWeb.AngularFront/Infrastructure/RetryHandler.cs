using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LastSeenWeb.AngularFront.Infrastructure
{
	public class RetryHandler : DelegatingHandler
	{
		private const int MaxRetries = 1;

		public RetryHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{ }

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HttpResponseMessage response = null;
			for (var i = 0; i < MaxRetries; i++)
			{
				response = await base.SendAsync(request, cancellationToken);
				if (response.IsSuccessStatusCode)
				{
					return response;
				}
			}

			return response;
		}
	}
}
