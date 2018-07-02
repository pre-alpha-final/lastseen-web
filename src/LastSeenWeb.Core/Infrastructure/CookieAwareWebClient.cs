using System;
using System.Net;

namespace LastSeenWeb.Core.Infrastructure
{
	public class CookieAwareWebClient : WebClient
	{
		private readonly CookieContainer _container = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request is HttpWebRequest)
			{
				(request as HttpWebRequest).CookieContainer = _container;
			}
			return request;
		}
	}
}
