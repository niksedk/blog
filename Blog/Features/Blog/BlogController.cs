using Blog.Features.Blog.Factories;
using Blog.Features.Blog.ViewModels;
using Blog.Features.Security;
using Blog.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Blog.Features.Blog
{
    [Produces("application/json")]
    [Route("api/Blog")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService,
                              IHttpContextAccessor httpContextAccessor,
                              IUserService userService) : base(httpContextAccessor, userService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("")]
        public IActionResult List(int fromDays = 0, int toDays = 100)
        {
            var list = _blogService.ListRecent(fromDays, toDays);
            return Ok(BlogEntryViewModelFactory.Make(list));
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("comments")]
        [Authorize(Roles = "admin")]
        public IActionResult ListComments()
        {
            var list = _blogService.ListComments();
            return Ok(BlogCommentViewModelFactory.Make(list));
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("{urlFriendlyId}")]
        public IActionResult Get(string urlFriendlyId)
        {
            var blogEntry = _blogService.GetFull(urlFriendlyId);
            return Ok(BlogEntryViewModelFactory.Make(blogEntry));
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("")]
        [Authorize(Roles = "admin")]
        public IActionResult AddBlogEntry([FromBody] AddBlogEntryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Body))
                return BadRequest();

            var user = BlogUser;
            if (user == null || !user.Claims.Any(p => p.Key == "role" && p.Value == "admin"))
                return Unauthorized();

            var blogEntry = _blogService.Add(user, request.Title, request.Body, request.CommentsDisabled);
            return Ok(BlogEntryViewModelFactory.Make(blogEntry));
        }

        [HttpPut]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int blogEntryId, UpdateBlogEntryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Body))
                return BadRequest();

            var user = BlogUser;
            if (user == null || !user.Claims.Any(p => p.Key == "role" && p.Value == "admin"))
                return Unauthorized();

            var blogEntry = _blogService.Update(user, blogEntryId, request.Title, request.Body, request.CommentsDisabled);
            return Ok(BlogEntryViewModelFactory.Make(blogEntry));
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int blogEntryId)
        {
            var user = BlogUser;
            if (user == null || !user.Claims.Any(p => p.Key == "role" && p.Value == "admin"))
                return Unauthorized();

            if (_blogService.Delete(user, blogEntryId))
                return NoContent();

            return NotFound();
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}/comments")]
        public IActionResult AddComment(int blogEntryId, [FromBody] AddCommentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Body))
                return BadRequest();

            var user = BlogUser;
            if (user == null && string.IsNullOrWhiteSpace(request.Name))
                return BadRequest();

            var comment = _blogService.AddComment(user, blogEntryId, request.Email, RemoteIpAddress, request.Body, request.Name);
            return Ok(BlogCommentViewModelFactory.Make(comment));
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("comments/{commentId}")]
        [Authorize]
        public IActionResult DeleteComment(int commentId)
        {
            var user = BlogUser;
            if (user == null || !user.Claims.Any(p => p.Key == "role" && p.Value == "admin"))
                return Unauthorized();

            if (_blogService.DeleteComment(user, commentId))
                return NoContent();

            return NotFound();
        }

    }
}