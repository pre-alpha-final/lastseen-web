using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class VoidResponse
	{
		[JsonProperty(PropertyName = "error")]
		public string Error { get; set; }
	}
}
