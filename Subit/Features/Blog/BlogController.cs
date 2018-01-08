using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubIt.Data.Security;
using SubIt.Features.Blog.ViewModels;
using SubIt.Features.Shared;

namespace SubIt.Features.Blog
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
            return Ok(list);
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [Route("{urlFriendlyId}")]
        public IActionResult Get(string urlFriendlyId)
        {
            var list = _blogService.GetFull(urlFriendlyId);
            return Ok(list);
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("")]
        public IActionResult AddBlogEntry([FromBody] AddBlogEntryRequest request)
        {
            var blogEntry = _blogService.Add(new SubItUser(), request.Title, request.Body);
            return Ok(blogEntry);
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}")]
        public IActionResult Delete(int blogEntryId)
        {
            if (_blogService.Delete(new SubItUser(), blogEntryId))
                return NoContent();

            return NotFound();
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}/comments")]
        public IActionResult AddComment(int blogEntryId, [FromBody] AddCommentRequest request)
        {
            var comment = _blogService.AddComment(new SubItUser(), blogEntryId, request.Email, RemoteIpAddress, request.Body);
            return Ok(comment);
        }

        [HttpDelete]
        [EnableCors("AllowAll")]
        [Route("{blogEntryId}/comments/{commentId}")]
        public IActionResult DeleteComment(int blogEntryId, int commentId)
        {
            if (_blogService.DeleteComment(new SubItUser(), blogEntryId, commentId))
                return NoContent();

            return NotFound();
        }

    }
}