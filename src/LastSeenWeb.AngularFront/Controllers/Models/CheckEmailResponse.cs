using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class CheckEmailResponse
	{
		[JsonProperty(PropertyName = "success")]
		public string Success { get; set; }
		[JsonProperty(PropertyName = "error")]
		public string Error { get; set; }
	}
}
