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
        [Route("register")]
        public IActionResult RegisterUser([FromBody] RegisterRequestViewModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new ErrorViewModel($"{nameof(request.Name)} cannot be empty", nameof(request.Name)));

            if (!EmailHelper.IsEmailValid(request.Email))
                return BadRequest(new ErrorViewModel($"{nameof(request.Email)} is not valid", nameof(request.Email)));

            if (string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new ErrorViewModel { Message = $"{nameof(request.Password)} cannot be empty" });

            if (!PasswordValidator.IsPassWordValid(request.Password))
                return BadRequest(new ErrorViewModel { Message = "Password must be between 8 and 64 characters and must contain both uppercase and lowercase letters" });

            // Trim spaces from name + email
            request.Name = request.Name.Trim();
            request.Email = request.Email.Trim();

            if (_userService.GetUser(request.Email) != null)
                return BadRequest(new ErrorViewModel($"A user with email {request.Email} already exists", nameof(request.Email)));

            var user = _userService.Register(request.Name, request.Email, request.ShowEmail, request.Password,
                                             string.Empty, RemoteIpAddress);
            return Ok(user);
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestViewModel request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel("Please provide credintials"));
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new ErrorViewModel($"Please provide '{nameof(request.Email)}'"));
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ErrorViewModel($"Please provide '{nameof(request.Password)}'"));
            }

            var user = _userService.GetUser(request.Email);
            if (user == null)
            {
                // use same message for "unknown user" and "wrong password" to make hacking harder
                return BadRequest(new ErrorViewModel("Incorrect login"));
            }

            var errorMessage = _userService.LoginPassword(user, request.Password, string.Empty);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new ErrorViewModel(errorMessage));
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