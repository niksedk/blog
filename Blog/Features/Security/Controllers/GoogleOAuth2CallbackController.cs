using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Security.Controllers
{
    /// <summary>
    /// https://developers.google.com/identity/sign-in/web/backend-auth
    /// </summary>
    [Produces("application/json")]
    public class GoogleOAuth2CallbackController : Controller
    {

        [HttpPost]
        [Route("login/google-tokensignin")]
        public IActionResult LoginViaGoogle(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return null;
            }
            string validateTokenUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=" + accessToken;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(validateTokenUrl).Result;
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                // deserialize to dto
            }

            //TODO: lookup/create user

            return null;
        }
    }
}