using Newtonsoft.Json;

namespace TD.Api.Dtos
{
    public class LoginRequest
    {
        private string mail;
        private string mdp;

        public LoginRequest(string mail, string mdp)
        {
            Email = mail;
            Password = mdp;
        }

        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}