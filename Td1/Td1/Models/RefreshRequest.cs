using Newtonsoft.Json;

namespace TD.Api.Dtos
{
    public class RefreshRequest
    {
        public RefreshRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}