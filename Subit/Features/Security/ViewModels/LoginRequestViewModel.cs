using Newtonsoft.Json;

namespace SubIt.Features.Security.ViewModels
{
    public class LoginRequestViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
