using System.Collections.Generic;
using Blog.Data.Blog;
using Blog.Features.Blog.ViewModels;

namespace Blog.Features.Blog.Factories
{
    internal static class BlogCommentViewModelFactory
    {
        internal static ICollection<BlogCommentViewModel> Make(ICollection<BlogComment> blogEntryComments)
        {
            var list = new List<BlogCommentViewModel>(blogEntryComments.Count);
            foreach (var blogComment in blogEntryComments)
            {
                list.Add(Make(blogComment));
            }
            return list;
        }

        internal static BlogCommentViewModel Make(BlogComment blogComment)
        {
            var vm = new BlogCommentViewModel
            {
                 Name = blogComment.CreatedBy != null ? blogComment.CreatedBy.Name : blogComment.Name,
                 Modified = blogComment.Modified,
                 Created = blogComment.Created,
                 Body = blogComment.Body,
                 UserId = blogComment.CreatedBy?.UserId,
                 BlogCommentId = blogComment.BlogCommentId,
                 Email = blogComment.Email,
            };
            if (blogComment.CreatedBy != null)
            {
                vm.Name = blogComment.CreatedBy.Name;
                vm.Email = blogComment.CreatedBy.Email;
            }
            return vm;
        }
    }
}
