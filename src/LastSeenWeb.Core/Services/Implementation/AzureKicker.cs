using System;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace LastSeenWeb.Core.Services.Implementation
{
	public class AzureKicker : IAzureKicker
	{
		private readonly IWebClientService _webClientService;
		private readonly IConfiguration _configuration;
		private const double DueTime = 0;
		private const double Period = 5;
		private Timer _timer;

		public AzureKicker(IWebClientService webClientService, IConfiguration configuration)
		{
			_webClientService = webClientService;
			_configuration = configuration;
		}

		public void Start()
		{
			if (_timer == null)
			{
				_timer = new Timer(OnTimerOnElapsed, null, TimeSpan.FromMinutes(DueTime), TimeSpan.FromMinutes(Period));
			}
		}

		private async void OnTimerOnElapsed(object state)
		{
			try
			{
				var link = $"https://{_configuration["Domain"]}/auth/login";
				await _webClientService.Get(link);
			}
			catch (Exception e)
			{
				// Ignore
			}
		}
	}
}
