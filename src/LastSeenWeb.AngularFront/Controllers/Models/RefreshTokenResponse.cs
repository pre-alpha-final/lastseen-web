using Newtonsoft.Json;

namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class RefreshTokenResponse
	{
		[JsonProperty(PropertyName = "refreshToken")]
		public string RefreshToken { get; set; }
	}
}
