using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class ForgotPasswordResponse
	{
		[JsonProperty(PropertyName = "error")]
		public string Error { get; set; }
	}
}
