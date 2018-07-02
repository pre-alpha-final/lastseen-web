namespace LastSeenWeb.Core.Extensions
{
	public static class StringExtensions
	{
		public static void AppendLine(this string s, string text)
		{
			s = $"{s}{(s.Length > 0 ? "\n" : string.Empty)}{text}";
		}
	}
}
