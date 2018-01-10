using Blog.Features.Blog.Factories;
using Blog.Features.Blog.ViewModels;
using Blog.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Features.Blog
{
    [Produces("application/json")]
    [Route("api/Blog")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
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
        public IActionResult ListComments()
        {
            var list = _blogService.ListComments();
            return Ok(list);
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
            var blogEntry = _blogService.Add(null, request.Title, request.Body, request.CommentsDisabled);
            return Ok(blogEntry);
        }

        [HttpPut]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int blogEntryId, UpdateBlogEntryRequest request)
        {
            var blogEntry = _blogService.Update(null, blogEntryId, request.Title, request.Body, request.CommentsDisabled);
            return Ok(blogEntry);
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int blogEntryId)
        {
            if (_blogService.Delete(null, blogEntryId))
                return NoContent();

            return NotFound();
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}/comments")]
        public IActionResult AddComment(int blogEntryId, [FromBody] AddCommentRequest request)
        {
            var comment = _blogService.AddComment(null, blogEntryId, request.Email, RemoteIpAddress, request.Body);
            return Ok(comment);
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("comments/{commentId}")]
        [Authorize]
        public IActionResult DeleteComment(int commentId)
        {
            if (_blogService.DeleteComment(null, commentId))
                return NoContent();

            return NotFound();
        }

    }
}