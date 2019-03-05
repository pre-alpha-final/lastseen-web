using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class CheckEmailResponse
	{
		[JsonProperty(PropertyName = "result")]
		public string Result { get; set; }
	}
}
