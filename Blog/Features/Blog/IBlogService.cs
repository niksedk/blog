using System.Collections.Generic;
using Blog.Data.Blog;
using Blog.Data.Security;

namespace Blog.Features.Blog
{
    public interface IBlogService
    {
        List<BlogEntry> ListRecent(int fromDaysBack, int toDaysBack);
        BlogEntry Update(BlogUser user, int blogEntryId, string title, string body, bool commentsDisabled);
        BlogEntry GetFull(string urlFriendlyId);
        BlogEntry Add(BlogUser subItUser, string title, string body, bool commentsDisabled);
        bool Delete(BlogUser user, int blogEntryId);

        List<BlogComment> ListComments(int blogEntryId);
        List<BlogComment> ListComments();
        BlogComment AddComment(BlogUser user, int blogEntryId, string email, string IpAddress, string body, string name);
        BlogComment UpdateComment(BlogUser user, int blogCommentId, string body);
        bool DeleteComment(BlogUser user, int blogCommentId);
    }
}
