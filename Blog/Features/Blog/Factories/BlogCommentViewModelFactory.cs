using System.Collections.Generic;
using Blog.Data.Blog;
using Blog.Features.Blog.ViewModels;

namespace Blog.Features.Blog.Factories
{
    public static class BlogCommentViewModelFactory
    {
        public static ICollection<BlogCommentViewModel> Make(ICollection<BlogComment> blogEntryComments)
        {
            var list = new List<BlogCommentViewModel>(blogEntryComments.Count);
            foreach (var blogComment in blogEntryComments)
            {
                list.Add(Make(blogComment));
            }
            return list;
        }

        private static BlogCommentViewModel Make(BlogComment blogComment)
        {
            return new BlogCommentViewModel
            {
                 Name = blogComment.CreatedBy != null ? blogComment.CreatedBy.Name : blogComment.Name,
                 Modified = blogComment.Modified,
                 Created = blogComment.Created,
                 Body = blogComment.Body,
                 UserId = blogComment.CreatedBy?.UserId,
                 BlogCommentId = blogComment.BlogCommentId,
                 Email = blogComment.Email,
            };
        }
    }
}
