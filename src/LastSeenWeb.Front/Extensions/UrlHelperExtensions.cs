using Microsoft.AspNetCore.Mvc;

namespace LastSeenWeb.Front.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
		{
			if (!urlHelper.IsLocalUrl(localUrl))
			{
				return urlHelper.Page("/Index");
			}

			return localUrl;
		}

		public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
		{
			return urlHelper.Page(
				"/Auth/ConfirmEmail",
				pageHandler: null,
				values: new { userId, code },
				protocol: scheme);
		}

		public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
		{
			return urlHelper.Page(
				"/Auth/ResetPassword",
				pageHandler: null,
				values: new { userId, code },
				protocol: scheme);
		}
	}
}
