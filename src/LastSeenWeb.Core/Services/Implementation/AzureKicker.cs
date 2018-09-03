using System;
using System.Threading;

namespace LastSeenWeb.Core.Services.Implementation
{
	public class AzureKicker : IAzureKicker
	{
		private readonly IWebClientService _webClientService;
		private const double DueTime = 0;
		private const double Period = 5;
		private Timer _timer;

		public AzureKicker(IWebClientService webClientService)
		{
			_webClientService = webClientService;
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
				var applicationSettings = new ApplicationSettings();
				var link = $"https://{applicationSettings.Domain}/Auth/Login?ReturnUrl=%2F";
				await _webClientService.Get(link);
			}
			catch (Exception e)
			{
				// Ignore
			}
		}
	}
}
