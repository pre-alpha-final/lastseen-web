namespace LastSeenWeb.Core.Infrastructure
{
	public class WebClientResult
	{
		public string Content { get; set; }
		public string DebugInfo { get; set; } = string.Empty;
		public bool Success { get; set; }
	}
}
