using Microsoft.AspNetCore.Mvc;

namespace LastSeenWeb.Front.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
		{
			return urlHelper.IsLocalUrl(localUrl)
				? localUrl
				: urlHelper.Page("/Index");
		}

		public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
		{
			return urlHelper.Page(
				"/Auth/ConfirmEmail",
				null,
				new { userId, code },
				scheme);
		}

		public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
		{
			return urlHelper.Page(
				"/Auth/ResetPassword",
				null,
				new { userId, code },
				scheme);
		}
	}
}
