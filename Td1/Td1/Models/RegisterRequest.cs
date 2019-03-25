using Newtonsoft.Json;

namespace TD.Api.Dtos
{
	public class RegisterRequest
	{
        /*private string mail;
        private string fisrtName;
        private string mdp;*/

        public RegisterRequest(string mail, string fisrtName, string lastName, string mdp)
        {
            Email = mail;
            FirstName = fisrtName;
            LastName = lastName;
            Password = mdp;
        }

        [JsonProperty("email")]
		public string Email { get; set; }
		
		[JsonProperty("first_name")]
		public string FirstName { get; set; }
		
		[JsonProperty("last_name")]
		public string LastName { get; set; }
		
		[JsonProperty("password")]
		public string Password { get; set; }
	}
}