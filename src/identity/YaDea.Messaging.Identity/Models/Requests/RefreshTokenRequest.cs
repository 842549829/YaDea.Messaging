using System.Text.Json.Serialization;

namespace YaDea.Messaging.Identity.Models.Requests
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
