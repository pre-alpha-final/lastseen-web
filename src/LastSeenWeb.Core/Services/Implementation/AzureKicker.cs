using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LastSeenWeb.Core.Services.Implementation
{
	public class AzureKicker : IAzureKicker
	{
		private readonly IWebClientService _webClientService;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AzureKicker> _logger;
		private const double Period = 5;
		private CancellationTokenSource _cancellationTokenSource;

		public AzureKicker(IWebClientService webClientService, IConfiguration configuration,
			ILogger<AzureKicker> logger)
		{
			_webClientService = webClientService;
			_configuration = configuration;
			_logger = logger;
		}

		public void Start()
		{
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource = new CancellationTokenSource();
			Task.Run(async () =>
			{
				try
				{
					var cancellationTokenSource = _cancellationTokenSource;
					while (true)
					{
						if (cancellationTokenSource.IsCancellationRequested)
						{
							return Task.CompletedTask;
						}
						await Kick();
						await Task.Delay(TimeSpan.FromMinutes(Period), cancellationTokenSource.Token);
					}
				}
				catch (Exception e)
				{
					_logger.LogError(e.Message);
					return Task.CompletedTask;
				}
			});
		}

		private async Task Kick()
		{
			try
			{
				var link = $"https://{_configuration["Domain"]}";
				await _webClientService.Get(link);
				_logger.LogDebug("Kicked");
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}
		}
	}
}
