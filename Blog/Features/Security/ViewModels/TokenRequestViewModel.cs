using System;
using Newtonsoft.Json;

namespace Blog.Features.Security.ViewModels
{
    public class TokenRequestViewModel
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        public string Code { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public Guid ClientSecret { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
