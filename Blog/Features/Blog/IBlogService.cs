using System.Collections.Generic;
using Blog.Data.Blog;
using Blog.Data.Security;

namespace Blog.Features.Blog
{
    public interface IBlogService
    {
        List<BlogEntry> ListRecent(int fromDaysBack, int toDaysBack);
        BlogEntry Update(SubItUser user, int blogEntryId, string title, string body, bool commentsDisabled);
        BlogEntry GetFull(string urlFriendlyId);
        BlogEntry Add(SubItUser subItUser, string title, string body, bool commentsDisabled);
        bool Delete(SubItUser user, int blogEntryId);

        List<BlogComment> ListComments(int blogEntryId);
        List<BlogComment> ListComments();
        BlogComment AddComment(SubItUser user, int blogEntryId, string email, string IpAddress, string body);
        BlogComment UpdateComment(SubItUser user, int blogCommentId, string body);
        bool DeleteComment(SubItUser user, int blogCommentId);
    }
}
