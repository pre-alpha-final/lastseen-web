using System;
using System.Threading;

namespace LastSeenWeb.Core.Services.Implementation
{
	public class AzureKicker : IAzureKicker
	{
		private readonly IWebClientService _webClientService;
		private readonly double _dueTime = 0;
		private readonly double _period = 5;
		private Timer _timer;

		public AzureKicker(IWebClientService webClientService)
		{
			_webClientService = webClientService;
		}

		public void Start()
		{
			if (_timer == null)
			{
				_timer = new Timer(OnTimerOnElapsed, null, TimeSpan.FromMinutes(_dueTime), TimeSpan.FromMinutes(_period));
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
