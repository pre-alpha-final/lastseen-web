using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class CheckEmailResponse
	{
		[JsonProperty(PropertyName = "successMessage")]
		public string SuccessMessage { get; set; }
		[JsonProperty(PropertyName = "errorMessage")]
		public string ErrorMessage { get; set; }
	}
}
