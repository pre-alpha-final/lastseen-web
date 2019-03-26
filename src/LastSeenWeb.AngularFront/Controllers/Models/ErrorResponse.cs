using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class ErrorResponse
	{
		[JsonProperty(PropertyName = "error")]
		public string Error { get; set; }
	}
}
