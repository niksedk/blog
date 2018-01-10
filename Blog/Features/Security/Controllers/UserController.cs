using System;
using Blog.Features.Security.ViewModels;
using Blog.Features.Shared;
using Blog.Features.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Security.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IHttpContextAccessor httpContextAccessor,
                              IUserService userService) : base(httpContextAccessor, userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("{userId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteUser(int userId)
        {
            if (_userService.Delete(userId))
                return NoContent();

            return NotFound();
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestViewModel request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Error = $"Please provide credintials"
                });
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new ErrorViewModel
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Error = $"Please provide '{nameof(request.Email)}'."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ErrorViewModel
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Error = $"Please provide '{nameof(request.Password)}'."
                });
            }

            var user = _userService.GetUser(request.Email);
            if (user == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Error = "Incorrect login" // use same message for "unknown user" and "wrong password" to make hacking harder
                });
            }

            var errorMessage = _userService.LoginPassword(user, request.Password, string.Empty);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new ErrorViewModel
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Error = errorMessage
                });
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