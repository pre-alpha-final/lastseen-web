using LastSeenWeb.Core.Extensions;
using LastSeenWeb.Core.Infrastructure;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LastSeenWeb.Core.Services.Implementation
{
	public class WebClientService : IWebClientService
	{
		private readonly ILogger<WebClientService> _logger;
		private readonly CookieAwareWebClient _webClient;
		private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

		public WebClientService(ILogger<WebClientService> logger)
		{
			_logger = logger;
			_webClient = new CookieAwareWebClient();
		}

		public async Task<WebClientResult> Get(string url, int retryCount = 10)
		{
			Task<string> MakeRequest()
			{
				_webClient.Headers["User-Agent"] =
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0";
				var result = _webClient.DownloadString(new Uri(url));

				return Task.FromResult(result);
			}

			using (await _semaphoreSlim.DisposableWaitAsync(TimeSpan.FromMinutes(10)))
			{
				return await RetryableRequest(MakeRequest, retryCount);
			}
		}

		public async Task<WebClientResult> Post(string url, NameValueCollection values, int retryCount = 5)
		{
			Task<string> MakeRequest()
			{
				_webClient.Headers["User-Agent"] =
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:58.0) Gecko/20100101 Firefox/58.0";
				_webClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
				var result = _webClient.UploadValues(url, "POST", values);

				return Task.FromResult(Encoding.UTF8.GetString(result));
			}

			using (await _semaphoreSlim.DisposableWaitAsync(TimeSpan.FromMinutes(10)))
			{
				return await RetryableRequest(MakeRequest, retryCount);
			}
		}

		private async Task<WebClientResult> RetryableRequest(Func<Task<string>> makeRequest, int retryCount)
		{
			var webClientResult = new WebClientResult();
			for (var attempt = 0; attempt < retryCount; attempt++)
			{
				try
				{
					if (attempt > 0)
					{
						webClientResult.DebugInfo
							= webClientResult.DebugInfo.AppendLine("retrying...");
					}
					webClientResult.Content = await makeRequest();
					webClientResult.DebugInfo
						= webClientResult.DebugInfo.AppendLine("Complete");
					webClientResult.Success = true;
					return webClientResult;
				}
				catch (WebException e)
				{
					var responseStream = e.Response?.GetResponseStream();
					if (responseStream != null)
					{
						using (var resp = new StreamReader(responseStream))
						{
							webClientResult.DebugInfo
								= webClientResult.DebugInfo.AppendLine(resp.ReadToEnd());
							_logger.LogError(webClientResult.DebugInfo);
						}
					}
					else
					{
						webClientResult.DebugInfo
							= webClientResult.DebugInfo.AppendLine($"{e.Message}");
						_logger.LogError(webClientResult.DebugInfo);
					}
				}
				catch (Exception e)
				{
					webClientResult.DebugInfo
						= webClientResult.DebugInfo.AppendLine($"{e.Message}");
					_logger.LogError(webClientResult.DebugInfo);
				}

				await Task.Delay(1000);
			}

			webClientResult.DebugInfo
				= webClientResult.DebugInfo.AppendLine("Failed");
			_logger.LogError(webClientResult.DebugInfo);
			return webClientResult;
		}
	}
}
