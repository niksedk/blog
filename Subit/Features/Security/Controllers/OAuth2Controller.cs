using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SubIt.Features.Security.ViewModels;

namespace SubIt.Features.Security.Controllers
{
    [Produces("application/json")]
    public class OAuth2Controller : Controller
    {
        private IJwt _jwt;

        public OAuth2Controller(IJwt jwt)
        {
            _jwt = jwt;
        }

        [HttpPost]
        [Route("api/token")]
        [EnableCors("AllowAll")]
        public IActionResult Index([FromBody] TokenRequestViewModel tokenRequest)
        {
            if (string.IsNullOrEmpty(tokenRequest?.GrantType))
                return BadRequest("invalid_request");

            //if (tokenRequest.GrantType == "authorization_code")
            //    return AuthorizationCode(tokenRequest);

            if (tokenRequest.GrantType == "refresh_token")
                return RefreshToken(tokenRequest);

            return BadRequest("unsupported_grant_type");
        }

        private IActionResult RefreshToken(TokenRequestViewModel tokenRequest)
        {
            if (string.IsNullOrEmpty(tokenRequest.ClientId))
            {
                return BadRequest("invalid_client");
            }

            if (string.IsNullOrEmpty(tokenRequest.RefreshToken))
            {
                return BadRequest("invalid_request");
            }

            string accessToken = "";
            //var accessToken = LoginHelper.LoginRefreshToken(tokenRequest.RefreshToken, tokenRequest.ClientId);

            if (accessToken == null)
            {
                //Logger.LogError($"Login *DENIED* via AccessTokenController.RefreshToken ({tokenRequest.refresh_token}) and client id ({tokenRequest.client_id}) from IP {PagesHelper.GetIpAddress()} - {PagesHelper.GetRawUrl()}", "API");
                return BadRequest("invalid_request");
            }

            //            Logger.LogRevisionTrack($"Login via AccessTokenController.RefreshToken ({tokenRequest.refresh_token}) and client id ({tokenRequest.client_id}) from IP {PagesHelper.GetIpAddress()}", "API");
            return GetTokenResponse(accessToken, tokenRequest.RefreshToken);
        }

        private IActionResult GetTokenResponse(string accessToken, string refreshToken)
        {
            return Ok(new TokenResponseViewModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600, // seconds 
                TokenType = "Bearer"
            });
        }

    }
}