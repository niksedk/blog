using Blog.Features.Security;
using Blog.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Log
{
    [Produces("application/json")]
    [Route("api/Logs/Referrers")]
    public class ReferrerLogController : BaseController
    {
        private readonly IReferrerLogRepository _referrerLogRepository;

        public ReferrerLogController(IReferrerLogRepository referrerLogRepository,
                                     IHttpContextAccessor httpContextAccessor,
                                     IUserService userService) : base(httpContextAccessor, userService)
        {
            _referrerLogRepository = referrerLogRepository;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Authorize(Roles = "admin")]
        [Route("")]
        public IActionResult List(int days = 100)
        {
            var list = _referrerLogRepository.ListRecent(days);
            return Ok(list);
        }

    }
}