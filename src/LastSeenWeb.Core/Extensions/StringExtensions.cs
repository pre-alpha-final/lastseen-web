namespace LastSeenWeb.Core.Extensions
{
	public static class StringExtensions
	{
		public static string AppendLine(this string s, string text)
		{
			return $"{s}{(s.Length > 0 ? "\n" : string.Empty)}{text}";
		}
	}
}
