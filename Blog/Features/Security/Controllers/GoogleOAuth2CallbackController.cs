using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Blog.Features.Security.ViewModels;
using Blog.Features.Shared;
using Blog.Features.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Security.Controllers
{
    /// <summary>
    /// https://developers.google.com/identity/sign-in/web/backend-auth
    /// </summary>
    [Produces("application/json")]
    public class GoogleOAuth2CallbackController : BaseController
    {
        private readonly IUserService _userService;

        public GoogleOAuth2CallbackController(IHttpContextAccessor httpContextAccessor,
                              IUserService userService) : base(httpContextAccessor, userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/users/google-tokensignin")]
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

            var user = _userService.GetUser("request.Email");
            if (user == null) //TODO: Make a Google provider check
            {
                // use same message for "unknown user" and "wrong password" to make hacking harder
                return BadRequest(new ErrorViewModel("Incorrect login"));
            }

            return Ok(new TokenResponseViewModel
            {
                AccessToken = _userService.GenerateJsonWebToken(user),
                ExpiresIn = 100,
                RefreshToken = Guid.NewGuid().ToString(),
                TokenType = "Bearer"
            });
        }
    }
}