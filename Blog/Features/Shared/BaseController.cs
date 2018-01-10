using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Blog.Data.Security;
using Blog.Features.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Blog.Features.Shared
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public BaseController(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public string RemoteIpAddress
        {
            get
            {
                string ip = GetHeaderValueAs<string>("X-Forwarded-For");
                
                if (string.IsNullOrWhiteSpace(ip) && _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress != null)
                    ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                if (string.IsNullOrWhiteSpace(ip))
                    ip = GetHeaderValueAs<string>("REMOTE_ADDR");

                if (string.IsNullOrWhiteSpace(ip))
                    ip = "unknown";

                return ip;
            }
        }

        public T GetHeaderValueAs<T>(string headerName)
        {
            StringValues values;
            if (_httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();   

                if (!string.IsNullOrEmpty(rawValues))
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default(T);
        }

        public SubItUser SubItUser
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (claim == null)
                    return null;

                return _userService.GetUser(int.Parse(claim.Value));
            }
        }

    }
}
